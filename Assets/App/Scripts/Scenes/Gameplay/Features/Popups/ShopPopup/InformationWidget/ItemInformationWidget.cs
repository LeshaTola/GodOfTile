using System.Collections.Generic;
using Assets.App.Scripts.Scenes.Gameplay.Features.Inventory.DTO;
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
        private string defaultText = "resources";

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

        public void UpdateInformation(TileConfig tileConfig)
        {
            Cleanup();
            if (tileConfig.Cost == null || tileConfig.Cost.Count <= 0)
            {
                header.Key = noCostText;
                header.Translate();
                return;
            }
            header.Key = defaultText;
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
