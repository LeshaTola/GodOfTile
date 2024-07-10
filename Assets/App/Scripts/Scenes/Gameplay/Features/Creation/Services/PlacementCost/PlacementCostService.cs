using Assets.App.Scripts.Scenes.Gameplay.Features.Creation.Providers;
using Assets.App.Scripts.Scenes.Gameplay.Features.Inventory.DTO;
using Assets.App.Scripts.Scenes.Gameplay.Features.Inventory.Systems;
using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Inventory.Services.PlacementCostServices
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

        private void ReduceResources(TileConfig tileConfig)
        {
            foreach (ResourceCount resourceCount in tileConfig.Cost)
            {
                inventorySystem.ChangeRecourseAmount(
                    resourceCount.Resource.ResourceName,
                    -resourceCount.Count
                );
            }
        }
    }
}
