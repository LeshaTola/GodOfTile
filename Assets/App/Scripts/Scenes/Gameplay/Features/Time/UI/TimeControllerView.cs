using System;
using App.Scripts.Modules.PopupAndViews.Views;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Scenes.Gameplay.Features.Time.UI
{
    public class TimeControllerView : AnimatedView
    {
        public event Action OnPauseButtonClicked;
        public event Action OnSpeed1ButtonClicked;
        public event Action OnSpeed2ButtonClicked;
        public event Action OnSpeed3ButtonClicked;

        [SerializeField] private Image selectorImage;

        [field: SerializeField] public Button PauseButton { get; private set; }
        [field: SerializeField] public Button Speed1Button { get; private set; }
        [field: SerializeField] public Button Speed2Button { get; private set; }
        [field: SerializeField] public Button Speed3Button { get; private set; }

        public void Initialize()
        {
            Cleanup();

            PauseButton.onClick.AddListener(() => OnPauseButtonClicked?.Invoke());
            Speed1Button.onClick.AddListener(() => OnSpeed1ButtonClicked?.Invoke());
            Speed2Button.onClick.AddListener(() => OnSpeed2ButtonClicked?.Invoke());
            Speed3Button.onClick.AddListener(() => OnSpeed3ButtonClicked?.Invoke());
        }

        public void Cleanup()
        {
            PauseButton.onClick.RemoveAllListeners();
            Speed1Button.onClick.RemoveAllListeners();
            Speed2Button.onClick.RemoveAllListeners();
            Speed3Button.onClick.RemoveAllListeners();
        }

        public void SetSelector(Button button)
        {
            var rectTransform = button.transform as RectTransform;
            selectorImage.rectTransform.anchoredPosition = rectTransform.anchoredPosition;
        }
    }
}