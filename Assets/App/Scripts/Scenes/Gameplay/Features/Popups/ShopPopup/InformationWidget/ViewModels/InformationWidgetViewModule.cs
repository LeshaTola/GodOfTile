using App.Scripts.Modules.Localization;
using App.Scripts.Scenes.Gameplay.Features.Inventory.Systems;
using App.Scripts.Scenes.Gameplay.Features.Shop.Factories.Cost;

namespace App.Scripts.Scenes.Gameplay.Features.Popups.ShopPopup.InformationWidget.ViewModels
{
    public class InformationWidgetViewModule : IInformationWidgetViewModule
    {
        public InformationWidgetViewModule(
            ILocalizationSystem localizationSystem,
            ICostUIFactory costUIFactory,
            IInventorySystem inventorySystem
        )
        {
            LocalizationSystem = localizationSystem;
            CostUIFactory = costUIFactory;
            InventorySystem = inventorySystem;
        }

        public ILocalizationSystem LocalizationSystem { get; }
        public ICostUIFactory CostUIFactory { get; }
        public IInventorySystem InventorySystem { get; }
    }
}