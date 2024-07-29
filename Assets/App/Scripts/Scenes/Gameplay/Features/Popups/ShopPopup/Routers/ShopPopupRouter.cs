using App.Scripts.Modules.Localization;
using App.Scripts.Modules.PopupLogic.General.Controllers;
using App.Scripts.Scenes.Gameplay.Features.Inventory.Systems;
using App.Scripts.Scenes.Gameplay.Features.Popups.ShopPopup.InformationWidget.ViewModels;
using App.Scripts.Scenes.Gameplay.Features.Popups.ShopPopup.ViewModels;
using App.Scripts.Scenes.Gameplay.Features.Shop.Factories.Cost;
using App.Scripts.Scenes.Gameplay.Features.Shop.Factories.Item;
using App.Scripts.Scenes.Gameplay.Features.Shop.Systems;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Scenes.Gameplay.Features.Popups.ShopPopup.Routers
{
    public class ShopPopupRouter : IShopPopupRouter
    {
        private IPopupController popupController;
        private ILocalizationSystem localizationSystem;
        private ICostUIFactory costUIFactory;
        private IItemFactory itemFactory;
        private IShopSystem shopSystem;
        private IInventorySystem inventorySystem;

        private ShopPopup popup;

        public ShopPopupRouter(
            IPopupController popupController,
            ILocalizationSystem localizationSystem,
            IItemFactory itemFactory,
            IShopSystem shopSystem,
            ICostUIFactory costUIFactory,
            IInventorySystem inventorySystem
        )
        {
            this.popupController = popupController;
            this.localizationSystem = localizationSystem;
            this.itemFactory = itemFactory;
            this.shopSystem = shopSystem;
            this.costUIFactory = costUIFactory;
            this.inventorySystem = inventorySystem;
        }

        public async UniTask ShowShopPopup()
        {
            popup = popupController.GetPopup<ShopPopup>();

            var informationWidgetViewModule = new InformationWidgetViewModule(
                localizationSystem,
                costUIFactory,
                inventorySystem
            );

            var viewModule = new ShopViewModule(
                localizationSystem,
                itemFactory,
                shopSystem,
                informationWidgetViewModule
            );
            popup.Setup(viewModule);

            await popup.Show();
        }

        public async UniTask HideShopPopup()
        {
            if (popup == null)
            {
                return;
            }

            await popup.Hide();
            popup = null;
        }
    }
}