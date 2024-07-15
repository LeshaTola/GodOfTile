using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Inventory.Services.PlacementCostServices
{
    public interface IPlacementCostService
    {
        void ProcessPlacementCost(TileConfig tileConfig);
    }
}