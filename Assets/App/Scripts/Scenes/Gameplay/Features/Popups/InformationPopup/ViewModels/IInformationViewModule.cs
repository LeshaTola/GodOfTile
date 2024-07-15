using Assets.App.Scripts.Scenes.Gameplay.Features.Commands.General;
using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using Module.Localization;

namespace Assets.App.Scripts.Features.Popups.InformationPopup.ViewModels
{
    public interface IInformationViewModule
    {
        TileConfig TileConfig { get; }
        ILocalizationSystem LocalizationSystem { get; }
        ILabeledCommand CloseCommand { get; }
    }
}