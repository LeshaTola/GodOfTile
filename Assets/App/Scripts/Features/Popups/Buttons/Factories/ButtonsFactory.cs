using UnityEngine;

namespace App.Scripts.Features.Popups.Buttons.Factories
{
    public class ButtonsFactory : IButtonsFactory
    {
        private PopupButton buttonTemplate;

        public ButtonsFactory(PopupButton buttonTemplate)
        {
            this.buttonTemplate = buttonTemplate;
        }

        public PopupButton GetButton()
        {
            return GameObject.Instantiate(buttonTemplate);
        }
    }
}