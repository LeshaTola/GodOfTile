using Assets.App.Scripts.Scenes.Gameplay.Features.Shop.Factories.Cost;
using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using Module.Localization;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Shop.UI.Information
{
    public class InformationWidgetViewModule : IInformationWidgetViewModule
    {
        public InformationWidgetViewModule(
            ILocalizationSystem localizationSystem,
            ICostUIFactory costUIFactory,
            TileConfig tileConfig
        )
        {
            LocalizationSystem = localizationSystem;
            CostUIFactory = costUIFactory;
            TileConfig = tileConfig;
        }

        public ILocalizationSystem LocalizationSystem { get; }
        public ICostUIFactory CostUIFactory { get; }
        public TileConfig TileConfig { get; }
    }
}
