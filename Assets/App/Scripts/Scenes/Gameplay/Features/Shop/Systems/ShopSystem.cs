using System.Collections.Generic;
using Assets.App.Scripts.Scenes.Gameplay.Features.Creation.Providers;
using Assets.App.Scripts.Scenes.Gameplay.Features.Inventory.Systems;
using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Providers.Collection;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Shop.Systems
{
    public class ShopSystem : IShopSystem
    {
        private IInventorySystem inventorySystem;
        private IActiveTileProvider activeTileProvider;
        private ITileCollectionProvider tileCollectionProvider;

        public List<TileConfig> AvailableTiles
        {
            get => tileCollectionProvider.Collection;
        }

        public ShopSystem(
            IInventorySystem inventorySystem,
            IActiveTileProvider activeTileProvider,
            ITileCollectionProvider tileCollectionProvider
        )
        {
            this.inventorySystem = inventorySystem;
            this.activeTileProvider = activeTileProvider;

            this.tileCollectionProvider = tileCollectionProvider;
        }

        public bool IsEnough(TileConfig tileConfig)
        {
            return inventorySystem.IsEnough(tileConfig.Cost);
        }

        public bool TryBuyTile(TileConfig tileConfig)
        {
            if (!inventorySystem.IsEnough(tileConfig.Cost))
            {
                return false;
            }

            BuyTile(tileConfig);
            return true;
        }

        public void BuyTile(TileConfig tileConfig)
        {
            activeTileProvider.ActiveTileConfig = tileConfig;
        }
    }
}
