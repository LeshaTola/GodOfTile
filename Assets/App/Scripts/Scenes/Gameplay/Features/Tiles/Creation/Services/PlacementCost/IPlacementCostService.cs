using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Services.PlacementCost
{
    public interface IPlacementCostService
    {
        void ProcessPlacementCost(TileConfig tileConfig);
        void ProcessPlacementCost();
    }
}