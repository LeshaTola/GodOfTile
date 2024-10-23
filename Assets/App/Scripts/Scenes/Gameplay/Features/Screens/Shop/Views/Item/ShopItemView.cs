using System;
using System.Collections.Generic;
using App.Scripts.Modules.PopupAndViews.Views;
using App.Scripts.Modules.Sounds;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace App.Scripts.Scenes.Gameplay.Features.Screens.Shop.Views.Item
{
    public class ShopItemView : AnimatedView, IPointerEnterHandler, IPointerExitHandler
    {
        public event Action OnBuyButtonClicked;
        public event Action OnPointerEntered;
        public event Action OnPointerExited;

        [SerializeField] private Image image;
        [SerializeField] private Button button;
        
        [SerializeField] private AudioDatabase audioDatabase;
        [field: ValueDropdown(nameof(GetKeys))]
        [field: SerializeField] public string ButtonSoundKey { get; private set; }

        public List<string> GetKeys()
        {
            if (audioDatabase == null)
            {
                return null;
            }
            return audioDatabase.GetKeys();
        }

        public void Initialize()
        {
            button.onClick.AddListener(() => OnBuyButtonClicked?.Invoke());
        }

        public void Cleanup()
        {
            button.onClick.RemoveAllListeners();
        }

        public void UpdateView(Sprite sprite)
        {
            image.sprite = sprite;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnPointerEntered?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnPointerExited?.Invoke();
        }
    }
}