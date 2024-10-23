using App.Scripts.Modules.StateMachine.Services.CleanupService;
using App.Scripts.Scenes.Gameplay.Features.Inventory.Systems;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Providers;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Services.TilesCreation;
using App.Scripts.Scenes.Gameplay.Features.Tiles.General;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Services.PlacementCost
{
    public class PlacementCostService : IPlacementCostService, ICleanupable
    {
        private IActiveTileProvider activeTileProvider;
        private ITilesCreationService tilesCreationService;
        private IInventorySystem inventorySystem;

        public PlacementCostService(
            IActiveTileProvider activeTileProvider,
            IInventorySystem inventorySystem,
            ITilesCreationService tilesCreationService
        )
        {
            this.activeTileProvider = activeTileProvider;
            this.inventorySystem = inventorySystem;
            this.tilesCreationService = tilesCreationService;
            
            tilesCreationService.OnTilePlaced += OnTilePlaced;
        }

        private void OnTilePlaced(Vector2Int position, Tile tile)
        {
            ProcessPlacementCost(tile.Config);
        }

        public void ProcessPlacementCost(TileConfig tileConfig)
        {
            ReduceResources(tileConfig);
            if (!inventorySystem.IsEnough(activeTileProvider.ActiveTileConfig.Cost))
            {
                activeTileProvider.ActiveTileConfig = null;
            }
        }

        public void ProcessPlacementCost()
        {
            ProcessPlacementCost(activeTileProvider.ActiveTileConfig);
        }

        private void ReduceResources(TileConfig tileConfig)
        {
            foreach (var resourceCount in tileConfig.Cost)
            {
                inventorySystem.ChangeRecourseAmount(
                    resourceCount.Resource.ResourceName,
                    -resourceCount.Count
                );
            }
        }

        public void Cleanup()
        {
            tilesCreationService.OnTilePlaced -= OnTilePlaced;
        }
    }
}