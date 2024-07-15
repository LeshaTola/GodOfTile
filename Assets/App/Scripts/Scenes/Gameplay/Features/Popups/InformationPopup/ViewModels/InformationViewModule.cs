using Assets.App.Scripts.Scenes.Gameplay.Features.Commands.General;
using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using Module.Localization;

namespace Assets.App.Scripts.Features.Popups.InformationPopup.ViewModels
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