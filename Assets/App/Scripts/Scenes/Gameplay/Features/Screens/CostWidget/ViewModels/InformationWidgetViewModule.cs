using App.Scripts.Modules.Factories;
using App.Scripts.Modules.Localization;
using App.Scripts.Scenes.Gameplay.Features.Inventory.Systems;
using App.Scripts.Scenes.Gameplay.Features.Popups.InformationWidget.Cost;

namespace App.Scripts.Scenes.Gameplay.Features.Popups.InformationWidget.ViewModels
{
    public class InformationWidgetViewModule : IInformationWidgetViewModule
    {
        public InformationWidgetViewModule(
            ILocalizationSystem localizationSystem,
            IFactory<CostUI> costUIFactory,
            IInventorySystem inventorySystem
        )
        {
            LocalizationSystem = localizationSystem;
            CostUIFactory = costUIFactory;
            InventorySystem = inventorySystem;
        }

        public ILocalizationSystem LocalizationSystem { get; }
        public IFactory<CostUI> CostUIFactory { get; }
        public IInventorySystem InventorySystem { get; }
    }
}