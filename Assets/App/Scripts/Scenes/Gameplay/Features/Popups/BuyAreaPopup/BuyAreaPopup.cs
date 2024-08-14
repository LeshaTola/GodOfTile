using App.Scripts.Features.Popups.Buttons;
using App.Scripts.Modules.Localization.Localizers;
using App.Scripts.Modules.PopupLogic.General.Popup;
using App.Scripts.Scenes.Gameplay.Features.Popups.BuyAreaPopup.ViewModules;
using App.Scripts.Scenes.Gameplay.Features.Popups.InformationWidget;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Popups.BuyAreaPopup
{
    public class BuyAreaPopup : Popup
    {
        [SerializeField] private TMProLocalizer header;
        [SerializeField] private ItemInformationWidget informationWidget;
        [SerializeField] private PopupButton buyButton;
        [SerializeField] private PopupButton closeButton;

        private IBuyAreaPopupViewModule viewModule;

        public void Setup(IBuyAreaPopupViewModule viewModule)
        {
            this.viewModule = viewModule;
            Cleanup();
            Initialize(viewModule);
            SetupUI(viewModule);
        }

        private void SetupUI(IBuyAreaPopupViewModule viewModule)
        {
            informationWidget.UpdateInformation(viewModule.Resources);
            buyButton.UpdateText(viewModule.BuyCommand.Label);

            Translate();
        }

        private void Initialize(IBuyAreaPopupViewModule viewModule)
        {
            this.viewModule = viewModule;

            header.Initialize(viewModule.LocalizationSystem);

            informationWidget.Initialize(this.viewModule.WidgetViewModule);

            buyButton.Initialize(viewModule.LocalizationSystem);
            closeButton.Initialize(viewModule.LocalizationSystem);

            buyButton.onButtonClicked += viewModule.BuyCommand.Execute;
            closeButton.onButtonClicked += viewModule.CloseCommand.Execute;
        }

        private void Translate()
        {
            header.Translate();

            informationWidget.Translate();

            buyButton.Translate();
            closeButton.Translate();
        }

        private void Cleanup()
        {
            if (viewModule == null)
            {
                return;
            }

            buyButton.onButtonClicked -= viewModule.BuyCommand.Execute;
            closeButton.onButtonClicked -= viewModule.CloseCommand.Execute;
        }
    }
}