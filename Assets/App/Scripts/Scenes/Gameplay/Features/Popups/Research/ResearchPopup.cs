using System;
using System.Collections.Generic;
using System.Linq;
using App.Scripts.Modules.Localization.Localizers;
using App.Scripts.Modules.PopupLogic.General.Popup;
using App.Scripts.Scenes.Gameplay.Features.Popups.Research.Elements;
using App.Scripts.Scenes.Gameplay.Features.Popups.Research.Elements.Level;
using App.Scripts.Scenes.Gameplay.Features.Popups.Research.Elements.Research;
using App.Scripts.Scenes.Gameplay.Features.Popups.Research.ViewModels;
using App.Scripts.Scenes.Gameplay.Features.Researches.Configs;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Popups.Research
{
    public class ResearchPopup : Popup
    {
        [SerializeField] private TMProLocalizer header;
        [SerializeField] private RectTransform levelsContainer;
        [SerializeField] private MoreInfoTab moreInfoTab;

        private ResearchPopupViewModule viewModule;
        private Dictionary<int, LevelElement> levelElements = new();

        public void Setup(ResearchPopupViewModule viewModule)
        {
            Cleanup();

            this.viewModule = viewModule;

            Initialize();
            SetupUI();
            Translate();
        }

        public void Initialize()
        {
            header.Initialize(viewModule.LocalizationSystem);
            moreInfoTab.Initialize(viewModule.LocalizationSystem, viewModule.InformationWidgetViewModule);
        }

        public void SetupUI()
        {
            foreach (var runtimeResearch in viewModule.ResearchService.Researches)
            {
                var researchElement = viewModule.ResearchesFactory.GetItem();
                researchElement.Setup(runtimeResearch.ResearchConfig);

                researchElement.OnResearchButtonClicked +=
                    () => ResearchElementOnOnResearchButtonClicked(runtimeResearch);

                ConnectToLevelElement(runtimeResearch.ResearchConfig.Level, researchElement);
            }

            SetupMoreInfoTab();
        }

        public void Translate()
        {
            header.Translate();
        }

        public void Cleanup()
        {
            if (viewModule == null)
            {
                return;
            }

            CleanupElements();

            viewModule = null;
        }

        private void ConnectToLevelElement(int level, ResearchElement researchElement)
        {
            if (!levelElements.ContainsKey(level))
            {
                var levelElement = viewModule.LevelsFactory.GetItem();
                SetupTransform(levelElement);
                levelElement.Setup(viewModule.LocalizationSystem, $"level: {level}");
                levelElements.Add(level, levelElement);
            }

            levelElements[level].AddResearch(researchElement);
        }

        private void SetupMoreInfoTab()
        {
            var activeResearch = viewModule.ResearchService.ActiveResearch;
            if (activeResearch != null)
            {
                SetResearch(activeResearch);
                return;
            }

            var research = viewModule.ResearchService.Researches.FirstOrDefault(x => x.IsCompleate == false);
            SetInactiveResearch(research);
        }

        private void SetInactiveResearch(RuntimeResearch runtimeResearch)
        {
            viewModule.ResearchService.OnTimerChanged -= OnResearchTimerChanged;
            moreInfoTab.UpdateInformation(runtimeResearch, () => { ResearchAction(runtimeResearch); });
        }

        private void ResearchAction(RuntimeResearch runtimeResearch)
        {
            if (!viewModule.InventorySystem.IsEnough(runtimeResearch.ResearchConfig.Cost))
            {
                return;
            }

            foreach (var resourceCount in runtimeResearch.ResearchConfig.Cost)
            {
                viewModule.InventorySystem.ChangeRecourseAmount(resourceCount.Resource.ResourceName,
                    -resourceCount.Count);
            }

            viewModule.ResearchService.StartResearch(runtimeResearch.ResearchConfig);
            moreInfoTab.UpdateTimerText(TimeSpan.FromSeconds(viewModule.ResearchService.Timer));
        }

        private void SetupTransform(LevelElement levelElement)
        {
            var levelTransform = levelElement.transform;
            levelTransform.SetParent(levelsContainer);
            levelTransform.localScale = Vector3.one;
            levelTransform.localPosition = Vector3.zero;
        }

        private void CleanupElements()
        {
            foreach (var levelElement in levelElements)
            {
                levelElement.Value.Cleanup();
            }
        }

        private void ResearchElementOnOnResearchButtonClicked(RuntimeResearch runtimeResearch)
        {
            SetResearch(runtimeResearch);
        }

        private void SetResearch(RuntimeResearch runtimeResearch)
        {
            var activeResearch = viewModule.ResearchService.ActiveResearch;
            if (activeResearch != null && activeResearch.ResearchConfig.Name.Equals(
                    runtimeResearch.ResearchConfig.Name))
            {
                viewModule.ResearchService.OnTimerChanged += OnResearchTimerChanged;
                moreInfoTab.UpdateInformation(runtimeResearch);
                return;
            }

            SetInactiveResearch(runtimeResearch);
        }

        private void OnResearchTimerChanged(float timer)
        {
            moreInfoTab.UpdateTimerText(TimeSpan.FromSeconds(timer));
        }
    }
}