using System.Collections.Generic;
using App.Scripts.Modules.Localization;
using App.Scripts.Modules.Localization.Localizers;
using App.Scripts.Modules.PopupAndViews.Views;
using App.Scripts.Scenes.Gameplay.Features.Screens.CostWidget.ViewModels;
using App.Scripts.Scenes.Gameplay.Features.Screens.Shop.Views.Item;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Screens.Shop.Views.ShopViews
{
    public class ShopView : AnimatedView
    {
        [SerializeField] private TMPLocalizer header;
        [SerializeField] private RectTransform container;
        
        public void Initialize(ILocalizationSystem localizationSystem, IInformationWidgetViewModule viewModule)
        {
            header.Initialize(localizationSystem);
        }

        public void Cleanup()
        {
            header.Cleanup();
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