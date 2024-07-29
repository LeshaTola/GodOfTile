using App.Scripts.Modules.Localization;
using App.Scripts.Scenes.Gameplay.Features.Commands.General;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;

namespace App.Scripts.Scenes.Gameplay.Features.Popups.InformationPopup.ViewModels
{
    public class InformationViewModule : IInformationViewModule
    {
        public InformationViewModule(
            ILocalizationSystem localizationSystem,
            ILabeledCommand goToGameplayStateCommand,
            TileConfig tileConfig
        )
        {
            LocalizationSystem = localizationSystem;
            CloseCommand = goToGameplayStateCommand;
            TileConfig = tileConfig;
        }

        public TileConfig TileConfig { get; }
        public ILocalizationSystem LocalizationSystem { get; }
        public ILabeledCommand CloseCommand { get; }
    }
}