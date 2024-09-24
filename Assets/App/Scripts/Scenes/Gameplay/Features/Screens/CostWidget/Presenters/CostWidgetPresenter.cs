using System.Collections.Generic;
using App.Scripts.Modules.Factories;
using App.Scripts.Modules.Localization;
using App.Scripts.Scenes.Gameplay.Features.Inventory.DTO;
using App.Scripts.Scenes.Gameplay.Features.Inventory.Systems;
using App.Scripts.Scenes.Gameplay.Features.Popups.InformationWidget.Cost;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Popups.InformationWidget.Presenters
{
    public class CostWidgetPresenter
    {
        private CostWidget costWidget;
        private IFactory<CostUI> costsFactory;
        private IInventorySystem inventorySystem;
        private ILocalizationSystem localizationSystem;


        public CostWidgetPresenter(
            CostWidget costWidget,
            IFactory<CostUI> costsFactory,
            IInventorySystem inventorySystem,
            ILocalizationSystem localizationSystem)
        {
            this.costWidget = costWidget;
            this.costsFactory = costsFactory;
            this.inventorySystem = inventorySystem;
            this.localizationSystem = localizationSystem;
        }

        public void Initialize()
        {
            costWidget.Initialize(localizationSystem);
        }

        public void Cleanup()
        {
            costWidget.Cleanup();
        }

        public void UpdateView(List<ResourceCount> resources)
        {
            List<CostUI> costs = new();

            for (var i = 0; i < resources.Count; i++)
            {
                var resourceCount = resources[i];
                
                var costUI = costsFactory.GetItem();
                bool isEnough = inventorySystem.IsEnough(resourceCount);
                costUI.UpdateUI(resourceCount.Resource.Sprite, resourceCount.Count,isEnough);
                costs.Add(costUI);
            } 
            
            costWidget.UpdateView(costs);
        }

        public void Move(Vector3 anchoredPosition)
        {
            costWidget.Move(anchoredPosition);
        }
        
        public void Show()
        {
            costWidget.Show();
        }

        public void Hide()
        {
            costWidget.Hide();
        }

    }
}