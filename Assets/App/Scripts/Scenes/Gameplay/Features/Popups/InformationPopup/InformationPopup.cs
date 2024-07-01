using Assets.App.Scripts.Features.Popups.Buttons;
using Assets.App.Scripts.Features.Popups.InformationPopup.ViewModels;
using Module.PopupLogic.General.Popups;
using UnityEngine;

namespace Assets.App.Scripts.Features.Popups.InformationPopup
{
    public class InformationPopup : Popup
    {
        [SerializeField]
        private PopupButton closeButton;

        private IInformationViewModule viewModule;

        public void Setup(IInformationViewModule viewModule)
        {
            this.viewModule = viewModule;

            closeButton.Init(viewModule.LocalizationSystem);
            closeButton.onButtonClicked += viewModule.CloseCommand.Execute;
        }

        public void Cleanup()
        {
            if (viewModule == null)
            {
                return;
            }

            closeButton.onButtonClicked -= viewModule.CloseCommand.Execute;
        }
    }
}
