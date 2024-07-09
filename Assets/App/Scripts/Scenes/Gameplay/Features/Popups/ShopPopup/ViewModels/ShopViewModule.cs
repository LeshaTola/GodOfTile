using Assets.App.Scripts.Scenes.Gameplay.Features.Shop.Factories.Item;
using Assets.App.Scripts.Scenes.Gameplay.Features.Shop.Systems;
using Assets.App.Scripts.Scenes.Gameplay.Features.Shop.UI.Information;
using Module.Localization;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Shop.UI
{
    public class ShopViewModule : IShopViewModule
    {
        public ShopViewModule(
            ILocalizationSystem localizationSystem,
            IItemFactory itemFactory,
            IShopSystem shopSystem,
            IInformationWidgetViewModule itemInformationWidget
        )
        {
            LocalizationSystem = localizationSystem;
            ItemFactory = itemFactory;
            ShopSystem = shopSystem;
            ItemInformationWidget = itemInformationWidget;
        }

        public ILocalizationSystem LocalizationSystem { get; }
        public IItemFactory ItemFactory { get; }
        public IShopSystem ShopSystem { get; }
        public IInformationWidgetViewModule ItemInformationWidget { get; }
    }
}
