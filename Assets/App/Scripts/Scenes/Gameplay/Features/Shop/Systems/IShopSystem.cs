using System;
using System.Collections.Generic;
using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Shop.Systems
{
    public interface IShopSystem
    {
        List<TileConfig> TilesToBuy { get; }

        event Action<TileConfig> OnNewTileAdd;

        void AddTile(TileConfig tileConfig);
        void BuyTile(TileConfig tileConfig);
        bool IsEnough(TileConfig tileConfig);
        bool TryBuyTile(TileConfig tileConfig);
    }
}
