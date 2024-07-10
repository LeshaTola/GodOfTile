using Assets.App.Scripts.Scenes.Gameplay.Features.Inventory.Systems;
using Assets.App.Scripts.Scenes.Gameplay.Features.Shop.Factories.Cost;
using Module.Localization;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Shop.UI.Information
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
