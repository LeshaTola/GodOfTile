using App.Scripts.Modules.Localization;
using App.Scripts.Scenes.Gameplay.Features.Commands.General;

namespace App.Scripts.Scenes.Gameplay.StateMachines.State.UI
{
    public class GameplayPopupViewModel : IGameplayPopupViewModel
    {
        public GameplayPopupViewModel(ILocalizationSystem localizationSystem, ILabeledCommand buildStateCommand,
            ILabeledCommand buyAreaStateCommand)
        {
            LocalizationSystem = localizationSystem;
            BuildStateCommand = buildStateCommand;
            BuyAreaStateCommand = buyAreaStateCommand;
        }

        public ILocalizationSystem LocalizationSystem { get; }
        public ILabeledCommand BuildStateCommand { get; }
        public ILabeledCommand BuyAreaStateCommand { get; }
    }
}