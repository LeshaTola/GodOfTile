using System;
using App.Scripts.Modules.StateMachine.Services.CleanupService;
using App.Scripts.Scenes.Gameplay.Features.Map.Providers.Grid;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Providers;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Providers.Effects;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Services.PlacementCost;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Services.Update;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Factories.Tiles;
using App.Scripts.Scenes.Gameplay.Features.Tiles.General;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Services;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.Views;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Services.TilesCreation
{
    public class TilesCreationService : ITilesCreationService, ICleanupable
    {
        public event Action<Vector2Int, Tile> OnTilePlaced;

        private IGridProvider gridProvider;
        private ITilesFactory tileFactory;
        private IActiveTileProvider activeTileProvider;
        private IPlacementCostService placementCostService;
        private ITileCreationEffectsProvider effectsService;
        private ITilesUpdateService updateService;
        private ISystemsService systemsService;
        private IEffectorVisualProvider effectorVisualProvider;
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
            IEffectorVisualProvider effectorVisualProvider,
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
            this.effectorVisualProvider = effectorVisualProvider;
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

            effectorVisualProvider.Cleanup();
            Object.Destroy(activeTile.gameObject);
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
            OnTilePlaced?.Invoke(tileBuffer.Position, tileBuffer);
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
            effectorVisualProvider.Cleanup();
            effectorVisualProvider.Setup(activeTile);
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