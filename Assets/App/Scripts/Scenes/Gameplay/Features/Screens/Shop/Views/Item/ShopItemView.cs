using System;
using App.Scripts.Modules.PopupAndViews.Views;
using App.Scripts.Scenes.Gameplay.Features.Popups.InformationWidget;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace App.Scripts.Scenes.Gameplay.Features.Popups.Shop.Item
{
    public class ShopItemView : AnimatedView, IPointerEnterHandler, IPointerExitHandler
    {
        public event Action OnBuyButtonClicked;
        public event Action OnPointerEntered;
        public event Action OnPointerExited;

        [SerializeField] private Image image;
        [SerializeField] private Button button;

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