using App.Scripts.Modules.Localization;
using App.Scripts.Scenes.Gameplay.Features.Commands.General;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Factories.TileSystemUIProvider;

namespace App.Scripts.Scenes.Gameplay.Features.Popups.Information.ViewModels
{
    public class InformationViewModule : IInformationViewModule
    {
        public InformationViewModule(
            ITileSystemUIProvidersFactory tileSystemUIProvidersFactory,
            ILocalizationSystem localizationSystem,
            ILabeledCommand goToGameplayStateCommand,
            TileConfig tileConfig
        )
        {
            TileSystemUIProvidersFactory = tileSystemUIProvidersFactory;
            LocalizationSystem = localizationSystem;
            CloseCommand = goToGameplayStateCommand;
            TileConfig = tileConfig;
        }

        public TileConfig TileConfig { get; }
        public ILocalizationSystem LocalizationSystem { get; }
        public ILabeledCommand CloseCommand { get; }
        public ITileSystemUIProvidersFactory TileSystemUIProvidersFactory { get; }
    }
}