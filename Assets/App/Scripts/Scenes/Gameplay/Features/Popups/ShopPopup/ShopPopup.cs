using System.Collections.Generic;
using Assets.App.Scripts.Scenes.Gameplay.Features.Shop.UI;
using Assets.App.Scripts.Scenes.Gameplay.Features.Shop.UI.Information;
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

        [SerializeField]
        private ItemInformationWidget informationWidget;

        private List<ShopItemUI> shopItems = new();
        private IShopViewModule viewModule;

        public void Setup(IShopViewModule viewModule)
        {
            Cleanup();

            this.viewModule = viewModule;
            informationWidget.Setup(viewModule.ItemInformationWidget);
            var tilesToBuy = viewModule.ShopSystem.AvailableTiles;
            AddItems(tilesToBuy.Count);

            header.Init(viewModule.LocalizationSystem);

            for (var i = 0; i < tilesToBuy.Count; i++)
            {
                SetupItem(shopItems[i], tilesToBuy[i]);
                shopItems[i].Show();
            }

            header.Translate();
        }

        public void Cleanup()
        {
            foreach (var item in shopItems)
            {
                item.Hide();
                item.onBuyButtonClicked -= BindBuyAction;
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
            var item = viewModule.ItemFactory.GetItem();
            item.transform.SetParent(container);
            item.transform.localPosition = Vector3.zero;
            item.transform.localScale = Vector3.one;
            item.Hide();

            shopItems.Add(item);
            return item;
        }

        private void SetupItem(ShopItemUI item, TileConfig tileConfig)
        {
            item.Setup(tileConfig, informationWidget);
            item.onBuyButtonClicked += BindBuyAction;
        }

        private void BindBuyAction(TileConfig tileConfig)
        {
            if (!viewModule.ShopSystem.IsEnough(tileConfig))
            {
                return;
            }

            viewModule.ShopSystem.BuyTile(tileConfig);
        }
    }
}