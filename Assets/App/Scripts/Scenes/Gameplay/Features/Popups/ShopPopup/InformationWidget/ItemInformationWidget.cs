using System.Collections.Generic;
using Assets.App.Scripts.Scenes.Gameplay.Features.Inventory.DTO;
using Assets.App.Scripts.Scenes.Gameplay.Features.Popups.ShopPopup.Configs;
using Assets.App.Scripts.Scenes.Gameplay.Features.Shop.UI.Cost;
using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using Module.Localization.Localizers;
using UnityEngine;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Shop.UI.Information
{
    public class ItemInformationWidget : MonoBehaviour
    {
        [SerializeField]
        private TMProLocalizer header;

        [SerializeField]
        private RectTransform container;

        [SerializeField]
        private InformationWidgetConfig config;

        private List<CostUI> costUIs = new();
        private IInformationWidgetViewModule viewModule;

        public void Setup(IInformationWidgetViewModule viewModule)
        {
            this.viewModule = viewModule;
            header.Init(viewModule.LocalizationSystem);
            header.Translate();
        }

        public void UpdateInformation(TileConfig tileConfig)
        {
            Cleanup();
            if (tileConfig.Cost == null || tileConfig.Cost.Count <= 0)
            {
                header.Key = config.NoCostText;
                header.Translate();
                return;
            }

            header.Key = config.DefaultText;
            header.Translate();

            AddCosts(tileConfig.Cost.Count);
            SetInformation(tileConfig.Cost);
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

            if (viewModule == null)
            {
                return;
            }
        }
    }
}