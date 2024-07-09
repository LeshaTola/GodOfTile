using System.Collections.Generic;
using Assets.App.Scripts.Scenes.Gameplay.Features.Shop.UI;
using Assets.App.Scripts.Scenes.Gameplay.Features.Shop.UI.Item;
using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using Module.Localization.Localizers;
using Module.PopupLogic.General.Popups;
using UnityEngine;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Popups.ShopPopup
{
    public class ShopPopup : Popup
    {
        [SerializeField]
        private TMProLocalizer header;

        [SerializeField]
        private RectTransform container;

        private List<ShopItemUI> shopItems = new();
        private IShopViewModule viewModule;

        public void Setup(IShopViewModule viewModule)
        {
            Cleanup();

            this.viewModule = viewModule;
            var tilesToBuy = viewModule.ShopSystem.TilesToBuy;
            AddItems(tilesToBuy.Count);

            header.Init(viewModule.LocalizationSystem);

            for (int i = 0; i < tilesToBuy.Count; i++)
            {
                SetupItem(shopItems[i], tilesToBuy[i]);
                shopItems[i].Show();
            }

            header.Translate();
        }

        public void Cleanup()
        {
            foreach (ShopItemUI item in shopItems)
            {
                item.Hide();
            }

            if (viewModule == null)
            {
                return;
            }
        }

        private void AddItems(int count)
        {
            while (shopItems.Count < count)
            {
                AddItem();
            }
        }

        private ShopItemUI AddItem()
        {
            ShopItemUI item = viewModule.ItemFactory.GetItem();
            item.transform.SetParent(container);
            item.transform.localPosition = Vector3.zero;
            item.transform.localScale = Vector3.one;
            item.Hide();

            shopItems.Add(item);
            return item;
        }

        private void SetupItem(ShopItemUI item, TileConfig tileConfig)
        {
            item.Setup(tileConfig, viewModule.InformationWidgetRouter);
            item.onBuyButtonClicked += () =>
            {
                if (!viewModule.ShopSystem.IsEnough(tileConfig.Cost))
                {
                    return;
                }
                viewModule.ShopSystem.BuyTile(tileConfig);
            };
        }
    }
}
