using System;
using System.Collections.Generic;
using Assets.App.Scripts.Scenes.Gameplay.Features.Creation.Providers;
using Assets.App.Scripts.Scenes.Gameplay.Features.Inventory.DTO;
using Assets.App.Scripts.Scenes.Gameplay.Features.Inventory.Systems;
using Assets.App.Scripts.Scenes.Gameplay.Features.Shop.Configs;
using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Shop.Systems
{
    public class ShopSystem : IShopSystem
    {
        public event Action<TileConfig> OnNewTileAdd;

        private IInventorySystem inventorySystem;
        private IActiveTileProvider activeTileProvider;
        private ShopConfig config;

        private List<TileConfig> tilesToBuy = new();

        public List<TileConfig> TilesToBuy
        {
            get => tilesToBuy;
        }

        public ShopSystem(
            IInventorySystem inventorySystem,
            IActiveTileProvider activeTileProvider,
            ShopConfig config
        )
        {
            this.inventorySystem = inventorySystem;
            this.activeTileProvider = activeTileProvider;
            this.config = config;

            Setup();
        }

        public void Setup()
        {
            foreach (var tile in config.StartTiles)
            {
                tilesToBuy.Add(tile);
            }
        }

        public void AddTile(TileConfig tileConfig)
        {
            tilesToBuy.Add(tileConfig);
            OnNewTileAdd?.Invoke(tileConfig);
        }

        public bool IsEnough(List<ResourceCount> resourcesCounts)
        {
            foreach (ResourceCount resourceCount in resourcesCounts)
            {
                if (
                    inventorySystem.IsEnough(
                        resourceCount.Resource.ResourceName,
                        resourceCount.Count
                    )
                )
                {
                    continue;
                }

                return false;
            }
            return true;
        }

        public void BuyTile(TileConfig tileConfig)
        {
            activeTileProvider.ActiveTileConfig = tileConfig;

            foreach (ResourceCount resourceCount in tileConfig.Cost)
            {
                inventorySystem.ChangeRecourseAmount(
                    resourceCount.Resource.ResourceName,
                    resourceCount.Count
                );
            }
        }
    }
}
