using System;
using App.Scripts.Modules.Localization;
using App.Scripts.Modules.Localization.Localizers;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Features.Popups.Buttons
{
    public class PopupButton : MonoBehaviour, IPopupButton
    {
        public event Action onButtonClicked;

        [SerializeField] private TMProLocalizer buttonText;
        [SerializeField] private Button button;

        public bool Interactable
        {
            get => button.interactable;
            set => button.interactable = value;
        }

        public void Initialize(ILocalizationSystem localizationSystem)
        {
            Cleanup();

            button.onClick.AddListener(() => onButtonClicked?.Invoke());

            if (buttonText != null)
            {
                buttonText.Initialize(localizationSystem);
            }
        }

        public void UpdateText(string text)
        {
            if (buttonText == null)
            {
                return;
            }
            
            buttonText.Key = text;
        }

        public void Translate()
        {
            if (buttonText == null)
            {
                return;
            }

            buttonText.Translate();
        }

        public void Cleanup()
        {
            button.onClick.RemoveAllListeners();
        }
    }
}