using System.Collections.Generic;
using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Shop.Systems
{
    public interface IShopSystem
    {
        List<TileConfig> AvailableTiles { get; }

        void BuyTile(TileConfig tileConfig);
        bool IsEnough(TileConfig tileConfig);
        bool TryBuyTile(TileConfig tileConfig);
    }
}
