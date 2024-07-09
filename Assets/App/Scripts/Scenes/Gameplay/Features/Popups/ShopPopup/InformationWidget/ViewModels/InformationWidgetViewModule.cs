using Assets.App.Scripts.Scenes.Gameplay.Features.Shop.Factories.Cost;
using Module.Localization;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Shop.UI.Information
{
    public class InformationWidgetViewModule : IInformationWidgetViewModule
    {
        public InformationWidgetViewModule(
            ILocalizationSystem localizationSystem,
            ICostUIFactory costUIFactory
        )
        {
            LocalizationSystem = localizationSystem;
            CostUIFactory = costUIFactory;
        }

        public ILocalizationSystem LocalizationSystem { get; }
        public ICostUIFactory CostUIFactory { get; }
    }
}
