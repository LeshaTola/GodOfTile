using System.Collections.Generic;
using Assets.App.Scripts.Scenes.Gameplay.Features.Inventory.DTO;
using Assets.App.Scripts.Scenes.Gameplay.Features.Shop.UI.Cost;
using Module.Localization.Localizers;
using Module.PopupLogic.General.Popups;
using UnityEngine;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Shop.UI.Information
{
    public class InformationWidgetPopup : Popup
    {
        [SerializeField]
        private TMProLocalizer header;

        [SerializeField]
        private RectTransform container;

        [SerializeField]
        private string noCostText = "is free!";

        private List<CostUI> costUIs = new();
        private IInformationWidgetViewModule viewModule;

        public void Setup(IInformationWidgetViewModule viewModule)
        {
            this.viewModule = viewModule;
            header.Init(viewModule.LocalizationSystem);
            header.Translate();
        }

        public void UpdateInformation(List<ResourceCount> resourcesCounts)
        {
            Cleanup();
            if (resourcesCounts == null || resourcesCounts.Count <= 0)
            {
                header.Key = noCostText;
                header.Translate();
                return;
            }

            AddCosts(resourcesCounts.Count);
            SetInformation(resourcesCounts);
        }

        private void SetInformation(List<ResourceCount> resourcesCounts)
        {
            for (int i = 0; i < resourcesCounts.Count; i++)
            {
                ResourceCount resourceCount = resourcesCounts[i];
                costUIs[i].UpdateUI(resourceCount.Resource.Sprite, resourceCount.Count);
                costUIs[i].Show();
            }
        }

        private void AddCosts(int count)
        {
            while (costUIs.Count < count)
            {
                AddCost();
            }
        }

        private void AddCost()
        {
            var costUI = viewModule.CostUIFactory.GetCostUI();
            costUI.transform.parent = container;
            costUI.Hide();
            costUIs.Add(costUI);
        }

        private void Cleanup()
        {
            foreach (var cost in costUIs)
            {
                cost.Hide();
            }

            if (viewModule == null)
            {
                return;
            }
        }
    }
}
