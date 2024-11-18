using System;
using App.Scripts.Modules.Localization;
using App.Scripts.Modules.Localization.Elements.Buttons;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Scenes.Gameplay.Features.Map.Items
{
    public class WorldChunkUI : MonoBehaviour
    {
        public event Action OnBuyButtonClicked;
        
        [SerializeField] private TMPLocalizedButton buyButton;
        
        public void Initialize(ILocalizationSystem localizationSystem)
        {
            buyButton.Initialize(localizationSystem);
            buyButton.UpdateAction(() => OnBuyButtonClicked?.Invoke());
            buyButton.Translate();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}