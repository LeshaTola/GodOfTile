using Assets.App.Scripts.Scenes.Gameplay.Features.Creation.Configs;
using Assets.App.Scripts.Scenes.Gameplay.Features.Creation.Factories;
using Assets.App.Scripts.Scenes.Gameplay.Features.Creation.Providers;
using Assets.App.Scripts.Scenes.Gameplay.Features.Creation.Services.Effects;
using Assets.App.Scripts.Scenes.Gameplay.Features.Creation.Services.Update;
using Assets.App.Scripts.Scenes.Gameplay.Features.Grid;
using Assets.App.Scripts.Scenes.Gameplay.Features.Inventory.Services.PlacementCostServices;
using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles;
using Cysharp.Threading.Tasks;
using Features.StateMachineCore;
using TileSystem;
using UnityEngine;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Creation.Services
{
    public class TilesCreationService : ITilesCreationService, ICleanupable
    {
        private IGridProvider gridProvider;
        private ITilesFactory tileFactory;
        private IActiveTileProvider activeTileProvider;
        private IPlacementCostService placementCostService;
        private ITileCreationEffectsProvider effectsService;
        private ITilesUpdateService updateService;
        private TilesCreationConfig config;

        private Tile activeTile;

        public TilesCreationService(
            IGridProvider gridProvider,
            ITilesFactory tileFactory,
            IActiveTileProvider activeTileProvider,
            IPlacementCostService placementCostService,
            ITileCreationEffectsProvider effectsService,
            ITilesUpdateService updateService,
            TilesCreationConfig config
        )
        {
            this.gridProvider = gridProvider;
            this.tileFactory = tileFactory;
            this.activeTileProvider = activeTileProvider;
            this.placementCostService = placementCostService;
            this.effectsService = effectsService;
            this.updateService = updateService;
            this.config = config;

            activeTileProvider.OnActiveTileChanged += OnActiveTileChanged;
        }

        public void StartPlacingTile()
        {
            if (activeTile != null || activeTileProvider.ActiveTileConfig == null)
            {
                return;
            }

            activeTile = tileFactory.GetTile(activeTileProvider.ActiveTileConfig);
        }

        public void StopPlacingTile()
        {
            if (activeTile == null)
            {
                return;
            }

            GameObject.Destroy(activeTile.gameObject);
            activeTile = null;
        }

        public void PlaceActiveTile()
        {
            if (activeTile == null || !gridProvider.IsValid(activeTile))
            {
                return;
            }

            for (int x = 0; x < activeTile.Config.Size.x; x++)
            {
                for (int y = 0; y < activeTile.Config.Size.y; y++)
                {
                    Vector2Int tileCoordinate =
                        new(activeTile.Position.x + x, activeTile.Position.y + y);
                    gridProvider.Grid[tileCoordinate.x, tileCoordinate.y] = activeTile;
                    updateService.UpdateConnectedTiles(tileCoordinate);
                }
            }

            var tileBuffer = activeTile;
            activeTile = null;

            PlayCreationVFX(tileBuffer).Forget();
            placementCostService.ProcessPlacementCost(tileBuffer.Config);
        }

        public void FullFill()
        {
            /*for (int x = 0; x < gridProvider.GridSize.x; x++)
            {
                for (int y = 0; y < gridProvider.GridSize.y; y++)
                {
                    var tile = tileFactory.GetTile(activeTileProvider.ActiveTileConfig);
                    tile.transform.position = new Vector3(x, 0, y);
                }
            }*/
        }

        public void MoveActiveTile(Vector2Int gridPosition)
        {
            if (activeTile == null)
            {
                return;
            }

            activeTile.transform.position = new(
                gridPosition.x,
                activeTile.transform.position.y,
                gridPosition.y
            );
            activeTile.Position = gridPosition;

            ChangeState();
        }

        public async UniTask RotateActiveTile()
        {
            if (activeTile == null)
            {
                return;
            }

            await activeTile.Visual.PlayRotation();
        }

        public void Cleanup()
        {
            activeTileProvider.OnActiveTileChanged -= OnActiveTileChanged;
        }

        private async UniTask PlayCreationVFX(Tile tile)
        {
            tile.Visual.SetState(TileState.Default);
            effectsService.PlayParticle(config.CreationParticleKey, tile.transform.position);
            await tile.Visual.PlayCreation();
        }

        private void ChangeState()
        {
            if (gridProvider.IsValid(activeTile))
            {
                activeTile.Visual.SetState(TileState.Correct);
            }
            else
            {
                activeTile.Visual.SetState(TileState.Wrong);
            }
        }

        private void OnActiveTileChanged()
        {
            StopPlacingTile();
            StartPlacingTile();
        }
    }
}
