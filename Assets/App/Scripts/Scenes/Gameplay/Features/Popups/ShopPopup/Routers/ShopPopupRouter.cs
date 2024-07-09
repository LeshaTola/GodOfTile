using Assets.App.Scripts.Features.Popups.InformationPopup.Routers;
using Assets.App.Scripts.Scenes.Gameplay.Features.Shop.Factories.Item;
using Assets.App.Scripts.Scenes.Gameplay.Features.Shop.Systems;
using Assets.App.Scripts.Scenes.Gameplay.Features.Shop.UI;
using Cysharp.Threading.Tasks;
using Module.Localization;
using Module.PopupLogic.General.Controller;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Popups.ShopPopup.Routers
{
    public class ShopPopupRouter : IShopPopupRouter
    {
        private IPopupController popupController;
        private ILocalizationSystem localizationSystem;
        private IInformationWidgetRouter informationWidgetRouter;
        private IItemFactory itemFactory;
        private IShopSystem shopSystem;

        private ShopPopup popup;

        public ShopPopupRouter(
            IPopupController popupController,
            ILocalizationSystem localizationSystem,
            IItemFactory itemFactory,
            IShopSystem shopSystem,
            IInformationWidgetRouter informationWidgetRouter
        )
        {
            this.popupController = popupController;
            this.localizationSystem = localizationSystem;
            this.itemFactory = itemFactory;
            this.shopSystem = shopSystem;
            this.informationWidgetRouter = informationWidgetRouter;
        }

        public async UniTask ShowShopPopup()
        {
            popup = popupController.GetPopup<ShopPopup>();
            var viewModule = new ShopViewModule(
                localizationSystem,
                itemFactory,
                shopSystem,
                informationWidgetRouter
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
