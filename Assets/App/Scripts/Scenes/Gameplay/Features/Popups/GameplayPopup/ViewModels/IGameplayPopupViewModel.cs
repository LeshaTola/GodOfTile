using App.Scripts.Modules.Localization;
using App.Scripts.Scenes.Gameplay.Features.Commands.General;

namespace App.Scripts.Scenes.Gameplay.StateMachines.State.UI
{
    public interface IGameplayPopupViewModel
    {
        ILocalizationSystem LocalizationSystem { get; }
        ILabeledCommand BuildStateCommand { get; }
        ILabeledCommand BuyAreaStateCommand { get; }
    }
}