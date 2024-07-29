using App.Scripts.Modules.StateMachine.Services.CleanupService;
using App.Scripts.Scenes.Gameplay.Features.Grid;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Providers;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Providers.Effects;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Services.PlacementCost;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Services.Update;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Factories.Tiles;
using App.Scripts.Scenes.Gameplay.Features.Tiles.General;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Services.TilesCreation
{
    public class TilesCreationService : ITilesCreationService, ICleanupable
    {
        private IGridProvider gridProvider;
        private ITilesFactory tileFactory;
        private IActiveTileProvider activeTileProvider;
        private IPlacementCostService placementCostService;
        private ITileCreationEffectsProvider effectsService;
        private ITilesUpdateService updateService;
        private ISystemsService systemsService;
        private TilesCreationConfig config;


        private Tile activeTile;

        public TilesCreationService(
            IGridProvider gridProvider,
            ITilesFactory tileFactory,
            IActiveTileProvider activeTileProvider,
            IPlacementCostService placementCostService,
            ITileCreationEffectsProvider effectsService,
            ITilesUpdateService updateService,
            ISystemsService systemsService,
            TilesCreationConfig config
        )
        {
            this.gridProvider = gridProvider;
            this.tileFactory = tileFactory;
            this.activeTileProvider = activeTileProvider;
            this.placementCostService = placementCostService;
            this.effectsService = effectsService;
            this.updateService = updateService;
            this.systemsService = systemsService;
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

            for (var x = 0; x < activeTile.Config.Size.x; x++)
            {
                for (var y = 0; y < activeTile.Config.Size.y; y++)
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
            systemsService.StartSystems(tileBuffer.Config);
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

            activeTile.transform.position = new Vector3(
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