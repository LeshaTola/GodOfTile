using System;
using Features.Popups.Languages;
using Module.Localization;
using Module.Localization.Localizers;
using UnityEngine;

namespace Assets.App.Scripts.Features.Popups.Buttons
{
    public class PopupButton : MonoBehaviour, IPopupButton
    {
        public event Action onButtonClicked;

        [SerializeField]
        private TMProLocalizer buttonText;

        [SerializeField]
        private UnityEngine.UI.Button button;

        public void UpdateText(string text)
        {
            buttonText.Key = text;
        }

        public void Init(ILocalizationSystem localizationSystem)
        {
            Cleanup();

            button.onClick.AddListener(() => onButtonClicked?.Invoke());
            buttonText.Init(localizationSystem);
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
