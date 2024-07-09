using Assets.App.Scripts.Scenes.Gameplay.Features.Shop.Factories.Item;
using Assets.App.Scripts.Scenes.Gameplay.Features.Shop.Systems;
using Assets.App.Scripts.Scenes.Gameplay.Features.Shop.UI.Information;
using Module.Localization;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Shop.UI
{
    public interface IShopViewModule
    {
        IItemFactory ItemFactory { get; }
        ILocalizationSystem LocalizationSystem { get; }
        IShopSystem ShopSystem { get; }
        IInformationWidgetViewModule ItemInformationWidget { get; }
    }
}
