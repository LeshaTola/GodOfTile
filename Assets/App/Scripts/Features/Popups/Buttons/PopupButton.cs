using System;
using App.Scripts.Modules.Localization;
using App.Scripts.Modules.Localization.Localizers;
using UnityEngine;

namespace App.Scripts.Features.Popups.Buttons
{
    public class PopupButton : MonoBehaviour, IPopupButton
    {
        public event Action onButtonClicked;

        [SerializeField]
        private TMProLocalizer buttonText;

        [SerializeField]
        private UnityEngine.UI.Button button;
        
        public void Initialize(ILocalizationSystem localizationSystem)
        {
            Cleanup();

            button.onClick.AddListener(() => onButtonClicked?.Invoke());
            buttonText.Initialize(localizationSystem);
        }  
        
        public void UpdateText(string text)
        {
            buttonText.Key = text;
        }

        public void Translate()
        {
            buttonText.Translate();
        }

        public void Cleanup()
        {
            button.onClick.RemoveAllListeners();
        }
    }
}