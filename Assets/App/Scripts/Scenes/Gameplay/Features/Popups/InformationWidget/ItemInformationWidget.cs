using System.Collections.Generic;
using App.Scripts.Modules.Localization.Localizers;
using App.Scripts.Scenes.Gameplay.Features.Inventory.DTO;
using App.Scripts.Scenes.Gameplay.Features.Popups.InformationWidget.Cost;
using App.Scripts.Scenes.Gameplay.Features.Popups.InformationWidget.ViewModels;
using App.Scripts.Scenes.Gameplay.Features.Popups.Shop.Configs;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Popups.InformationWidget
{
    public class ItemInformationWidget : MonoBehaviour
    {
        [SerializeField] private TMProLocalizer header;

        [SerializeField]
        private RectTransform container;

        [SerializeField]
        private InformationWidgetConfig config;

        private List<CostUI> costUIs = new();
        private IInformationWidgetViewModule viewModule;

        public void Initialize(IInformationWidgetViewModule viewModule)
        {
            this.viewModule = viewModule;
            header.Initialize(viewModule.LocalizationSystem);
        }

        public void UpdateInformation(List<ResourceCount> resources)
        {
            Cleanup();
            if (resources == null || resources.Count <= 0)
            {
                header.Key = config.NoCostText;
                return;
            }

            header.Key = config.DefaultText;

            AddCosts(resources.Count);
            SetInformation(resources);
        }

        public void Translate()
        {
            header.Translate();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        private void SetInformation(List<ResourceCount> resourcesCounts)
        {
            for (var i = 0; i < resourcesCounts.Count; i++)
            {
                var resourceCount = resourcesCounts[i];

                var textColor = config.TextColorConfig.DefaultColor;
                if (!viewModule.InventorySystem.IsEnough(resourceCount))
                {
                    textColor = config.TextColorConfig.WrongColor;
                }

                costUIs[i].UpdateUI(resourceCount.Resource.Sprite, resourceCount.Count, textColor);
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
            costUI.transform.SetParent(container);

            costUI.transform.localPosition = Vector3.zero;
            costUI.transform.localScale = Vector3.one;

            costUI.Hide();
            costUIs.Add(costUI);
        }

        private void Cleanup()
        {
            foreach (var cost in costUIs)
            {
                cost.Hide();
            }
        }
    }
}