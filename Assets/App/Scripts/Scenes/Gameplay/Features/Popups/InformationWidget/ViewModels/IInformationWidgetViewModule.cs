using App.Scripts.Modules.Localization;
using App.Scripts.Scenes.Gameplay.Features.Inventory.Systems;
using App.Scripts.Scenes.Gameplay.Features.Shop.Factories.Cost;

namespace App.Scripts.Scenes.Gameplay.Features.Popups.InformationWidget.ViewModels
{
    public interface IInformationWidgetViewModule
    {
        ICostUIFactory CostUIFactory { get; }
        ILocalizationSystem LocalizationSystem { get; }
        IInventorySystem InventorySystem { get; }
    }
}