using App.Scripts.Modules.Localization;
using App.Scripts.Scenes.Gameplay.Features.Popups.InformationWidget.ViewModels;
using App.Scripts.Scenes.Gameplay.Features.Shop.Factories.Item;
using App.Scripts.Scenes.Gameplay.Features.Shop.Systems;

namespace App.Scripts.Scenes.Gameplay.Features.Popups.Shop.ViewModels
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