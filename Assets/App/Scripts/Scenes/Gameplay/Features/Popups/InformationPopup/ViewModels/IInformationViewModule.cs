using App.Scripts.Modules.Localization;
using App.Scripts.Scenes.Gameplay.Features.Commands.General;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;

namespace App.Scripts.Scenes.Gameplay.Features.Popups.InformationPopup.ViewModels
{
    public interface IInformationViewModule
    {
        TileConfig TileConfig { get; }
        ILocalizationSystem LocalizationSystem { get; }
        ILabeledCommand CloseCommand { get; }
    }
}