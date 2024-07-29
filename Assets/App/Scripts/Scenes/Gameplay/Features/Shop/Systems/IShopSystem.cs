using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;

namespace App.Scripts.Scenes.Gameplay.Features.Shop.Systems
{
    public interface IShopSystem
    {
        List<TileConfig> AvailableTiles { get; }

        void BuyTile(TileConfig tileConfig);
        bool IsEnough(TileConfig tileConfig);
        bool TryBuyTile(TileConfig tileConfig);
    }
}