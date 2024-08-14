using System.Collections.Generic;
using App.Scripts.Modules.Localization.Localizers;
using App.Scripts.Modules.PopupLogic.General.Popup;
using App.Scripts.Scenes.Gameplay.Features.Popups.InformationWidget;
using App.Scripts.Scenes.Gameplay.Features.Popups.ShopPopup.Item;
using App.Scripts.Scenes.Gameplay.Features.Popups.ShopPopup.ViewModels;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Popups.ShopPopup
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
            informationWidget.Initialize(viewModule.ItemInformationWidget);
            var tilesToBuy = viewModule.ShopSystem.AvailableTiles;
            AddItems(tilesToBuy.Count);

            header.Initialize(viewModule.LocalizationSystem);

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