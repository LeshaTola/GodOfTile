using System.Collections.Generic;
using App.Scripts.Modules.Localization;
using App.Scripts.Modules.Localization.Localizers;
using App.Scripts.Scenes.Gameplay.Features.Screens.CostWidget.Configs;
using App.Scripts.Scenes.Gameplay.Features.Screens.CostWidget.Cost;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Screens.CostWidget
{
    public class CostWidget : MonoBehaviour
    {
        [SerializeField] private TMPLocalizer header;
        [SerializeField] private RectTransform container;
        [SerializeField] private InformationWidgetConfig config;

        public void Initialize(ILocalizationSystem localizationSystem)
        {
            header.Initialize(localizationSystem);
        }

        public void Cleanup()
        {
            header.Cleanup();
        }

        public void UpdateView(List<CostUI> costs)
        {
            CleanupView();

            if (costs == null)
            {
                SetHeader(0);
                return;
            }

            SetHeader(costs.Count);

            foreach (var costUI in costs)
            {
                costUI.transform.SetParent(container, false);
            }
        }

        public void Move(Vector3 anchoredPosition)
        {
            transform.position = anchoredPosition;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        private void SetHeader(int count)
        {
            if (count <= 0)
            {
                header.Key = config.NoCostText;
                header.Translate();
                return;
            }

            header.Key = config.DefaultText;
            header.Translate();
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