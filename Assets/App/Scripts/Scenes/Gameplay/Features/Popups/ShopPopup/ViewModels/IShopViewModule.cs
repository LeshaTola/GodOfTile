using Assets.App.Scripts.Features.Popups.InformationPopup.Routers;
using Assets.App.Scripts.Scenes.Gameplay.Features.Shop.Factories.Item;
using Assets.App.Scripts.Scenes.Gameplay.Features.Shop.Systems;
using Module.Localization;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Shop.UI
{
    public interface IShopViewModule
    {
        IItemFactory ItemFactory { get; }
        ILocalizationSystem LocalizationSystem { get; }
        IShopSystem ShopSystem { get; }
        IInformationWidgetRouter InformationWidgetRouter { get; }
    }
}
