using App.Scripts.Modules.Factories;
using App.Scripts.Modules.Localization;
using App.Scripts.Scenes.Gameplay.Features.Inventory.Systems;
using App.Scripts.Scenes.Gameplay.Features.Popups.InformationWidget.Cost;

namespace App.Scripts.Scenes.Gameplay.Features.Popups.InformationWidget.ViewModels
{
    public interface IInformationWidgetViewModule
    {
        IFactory<CostUI> CostUIFactory { get; }
        ILocalizationSystem LocalizationSystem { get; }
        IInventorySystem InventorySystem { get; }
    }
}