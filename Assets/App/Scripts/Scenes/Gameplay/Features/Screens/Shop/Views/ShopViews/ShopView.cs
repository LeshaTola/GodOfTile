using System;
using System.Collections.Generic;
using App.Scripts.Modules.Localization;
using App.Scripts.Modules.Localization.Localizers;
using App.Scripts.Modules.PopupAndViews.Views;
using App.Scripts.Scenes.Gameplay.Features.Screens.CostWidget.ViewModels;
using App.Scripts.Scenes.Gameplay.Features.Screens.Shop.Views.Item;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Scenes.Gameplay.Features.Screens.Shop.Views.ShopViews
{
    public class ShopView : AnimatedView
    {
        public event Action OnInfoButtonClicked;
        
        [SerializeField] private TMPLocalizer header;
        [SerializeField] private RectTransform container;
        [SerializeField] private Button infoButton;
        
        public void Initialize(ILocalizationSystem localizationSystem)
        {
            header.Initialize(localizationSystem);
            infoButton.onClick.AddListener(() => OnInfoButtonClicked?.Invoke());
        }

        public void Cleanup()
        {
            header.Cleanup();
            infoButton.onClick.RemoveAllListeners();
        }

        public void Translate()
        {
            header.Translate();
        }

        public void AddItemView(ShopItemView shopItemView)
        {
            shopItemView.transform.SetParent(container,false);
        }

        private void CleanupView()
        {
            foreach (Transform child in container)
            {
                Destroy(child.gameObject);
            }
        }
    }

}