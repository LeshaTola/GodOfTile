using Assets.App.Scripts.Features.Popups.InformationPopup.Routers;
using Assets.App.Scripts.Scenes.Gameplay.Features.Shop.Factories.Item;
using Assets.App.Scripts.Scenes.Gameplay.Features.Shop.Systems;
using Module.Localization;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Shop.UI
{
    public class ShopViewModule : IShopViewModule
    {
        public ShopViewModule(
            ILocalizationSystem localizationSystem,
            IItemFactory itemFactory,
            IShopSystem shopSystem,
            IInformationWidgetRouter infoWidgetRouter
        )
        {
            LocalizationSystem = localizationSystem;
            ItemFactory = itemFactory;
            ShopSystem = shopSystem;
            InformationWidgetRouter = infoWidgetRouter;
        }

        public ILocalizationSystem LocalizationSystem { get; }
        public IItemFactory ItemFactory { get; }
        public IShopSystem ShopSystem { get; }
        public IInformationWidgetRouter InformationWidgetRouter { get; }
    }
}
