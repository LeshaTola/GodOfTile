using App.Scripts.Scenes.Gameplay.Features.Inventory.Systems;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Providers;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Services.PlacementCost
{
    public class PlacementCostService : IPlacementCostService
    {
        private IActiveTileProvider activeTileProvider;
        private IInventorySystem inventorySystem;

        public PlacementCostService(
            IActiveTileProvider activeTileProvider,
            IInventorySystem inventorySystem
        )
        {
            this.activeTileProvider = activeTileProvider;
            this.inventorySystem = inventorySystem;
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
    }
}