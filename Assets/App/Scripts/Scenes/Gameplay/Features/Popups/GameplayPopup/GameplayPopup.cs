using App.Scripts.Features.Popups.Buttons;
using App.Scripts.Modules.PopupLogic.General.Popup;
using App.Scripts.Scenes.Gameplay.Features.Popups.GameplayPopup.ViewModels;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Popups.GameplayPopup
{
    public class GameplayPopup: Popup
    {
        [SerializeField] private PopupButton buildStateButton;
        [SerializeField] private PopupButton buyAreaStateButton;

        private IGameplayPopupViewModel viewModel;
        
        public void Setup(IGameplayPopupViewModel viewModel)
        {
            Cleanup();
            this.viewModel = viewModel;
            
            Initialize();
            SetupInternal();
            Translate();
        }

        private void Initialize()
        {
            buildStateButton.Initialize(viewModel.LocalizationSystem);
            buyAreaStateButton.Initialize(viewModel.LocalizationSystem);
        }

        private void SetupInternal()
        {
            buildStateButton.onButtonClicked += viewModel.BuildStateCommand.Execute;
            buyAreaStateButton.onButtonClicked += viewModel.BuyAreaStateCommand.Execute;
            
            buildStateButton.UpdateText(viewModel.BuildStateCommand.Label);
            buyAreaStateButton.UpdateText(viewModel.BuyAreaStateCommand.Label);
        }

        private void Translate()
        {
            buildStateButton.Translate();
            buyAreaStateButton.Translate();
        }

        private void Cleanup()
        {
            if (viewModel == null)
            {
                return;
            }

            buildStateButton.onButtonClicked -= viewModel.BuildStateCommand.Execute;
            buyAreaStateButton.onButtonClicked -= viewModel.BuyAreaStateCommand.Execute;
        }

    }
}