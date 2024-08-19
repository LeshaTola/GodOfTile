using System;
using System.Collections.Generic;
using App.Scripts.Modules.StateMachine.Services.CleanupService;
using App.Scripts.Scenes.Gameplay.Features.Inventory.Systems;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Providers;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Providers.Collection;

namespace App.Scripts.Scenes.Gameplay.Features.Shop.Systems
{
    public class ShopSystem : IShopSystem, ICleanupable
    {
        public event Action<TileConfig> OnNewTileAdd;
        
        private IInventorySystem inventorySystem;
        private IActiveTileProvider activeTileProvider;
        private ITileCollectionProvider tileCollectionProvider;

        public List<TileConfig> AvailableTiles => tileCollectionProvider.Collection;

        public ShopSystem(
            IInventorySystem inventorySystem,
            IActiveTileProvider activeTileProvider,
            ITileCollectionProvider tileCollectionProvider
        )
        {
            this.inventorySystem = inventorySystem;
            this.activeTileProvider = activeTileProvider;

            this.tileCollectionProvider = tileCollectionProvider;
            tileCollectionProvider.OnNewTileAdd += NewTileAdded;
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

        public void Cleanup()
        {
            tileCollectionProvider.OnNewTileAdd -= NewTileAdded;
        }
        

        private void NewTileAdded(TileConfig tileConfig)
        {
            OnNewTileAdd?.Invoke(tileConfig);
        }
    }
}