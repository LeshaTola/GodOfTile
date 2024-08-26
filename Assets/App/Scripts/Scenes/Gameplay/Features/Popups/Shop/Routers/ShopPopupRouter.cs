using App.Scripts.Modules.Localization;
using App.Scripts.Modules.PopupLogic.General.Controllers;
using App.Scripts.Scenes.Gameplay.Features.Popups.InformationWidget.ViewModels;
using App.Scripts.Scenes.Gameplay.Features.Popups.Shop.ViewModels;
using App.Scripts.Scenes.Gameplay.Features.Shop.Factories.Item;
using App.Scripts.Scenes.Gameplay.Features.Shop.Systems;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Scenes.Gameplay.Features.Popups.Shop.Routers
{
    public class ShopPopupRouter : IShopPopupRouter
    {
        private IPopupController popupController;
        private ILocalizationSystem localizationSystem;
        private IItemFactory itemFactory;
        private IShopSystem shopSystem;
        private IInformationWidgetViewModule informationWidgetViewModule;

        private ShopPopup popup;

        public ShopPopupRouter(
            IPopupController popupController,
            ILocalizationSystem localizationSystem,
            IItemFactory itemFactory,
            IShopSystem shopSystem, 
            IInformationWidgetViewModule informationWidgetViewModule
        )
        {
            this.popupController = popupController;
            this.localizationSystem = localizationSystem;
            this.itemFactory = itemFactory;
            this.informationWidgetViewModule = informationWidgetViewModule;
            this.shopSystem = shopSystem;
        }

        public async UniTask ShowShopPopup()
        {
            popup = popupController.GetPopup<ShopPopup>();

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