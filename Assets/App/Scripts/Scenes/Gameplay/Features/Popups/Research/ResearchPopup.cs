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
            
            viewModule.ResearchService.OnResearchCompleted += OnResearchCompleted;
            viewModule.ResearchService.OnLevelChanged += OnLevelChanged;
        }

        private void SetupElements()
        {
            foreach (var runtimeResearch in viewModule.ResearchService.Researches)
            {
                var researchElement = viewModule.ResearchesFactory.GetItem();
                researchElement.Setup(runtimeResearch);

                researchElement.OnResearchButtonClicked +=
                    () => ResearchElementOnOnResearchButtonClicked(runtimeResearch);

                ConnectToLevelElement(runtimeResearch.ResearchConfig.Level, researchElement);
            }

            for (var i = 1; i <= viewModule.ResearchService.Level; i++)
            {
                if (!levelElements.ContainsKey(i))
                {
                    return;
                }

                levelElements[i].Open();
            }
        }

        public void SetupUI()
        {
            SetupElements();
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
            viewModule.ResearchService.OnLevelChanged -= OnLevelChanged;
            viewModule.ResearchService.OnResearchCompleted -= OnResearchCompleted;
            viewModule = null;
        }

        private void SetupMoreInfoTab()
        {
            var research = viewModule.ResearchService.ActiveResearch;
            if (research == null)
            {
                int level = viewModule.ResearchService.Level;
                var researches = viewModule.ResearchService.Researches;
                var availableResearches
                    = researches.Where(x => x.ResearchConfig.Level <= level).ToList();

                research = availableResearches.FirstOrDefault(x => x.IsCompleate == false);
                if (research == null)
                {
                    research = availableResearches.First();
                }
            }

            SetResearch(research);
        }

        private void SetResearch(RuntimeResearch runtimeResearch)
        {
            moreInfoTab.UpdateInformation(runtimeResearch.ResearchConfig);

            var activeResearch = viewModule.ResearchService.ActiveResearch;
            if (activeResearch != null
                && activeResearch.ResearchConfig.Name.Equals(runtimeResearch.ResearchConfig.Name))
            {
                viewModule.ResearchService.OnTimerChanged += SetButtonTimer;

                SetButtonTimer(viewModule.ResearchService.Timer);
                return;
            }

            if (runtimeResearch.IsCompleate)
            {
                moreInfoTab.UpdateButton("It has already been researched");
                return;
            }

            viewModule.ResearchService.OnTimerChanged -= SetButtonTimer;
            moreInfoTab.UpdateButton("buy", () => { ResearchAction(runtimeResearch); });
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

            viewModule.ResearchService.OnTimerChanged += SetButtonTimer;
            viewModule.ResearchService.StartResearch(runtimeResearch.ResearchConfig);
            SetButtonTimer(viewModule.ResearchService.Timer);
        }

        private void ConnectToLevelElement(int level, ResearchElement researchElement)
        {
            if (!levelElements.ContainsKey(level))
            {
                var levelElement = viewModule.LevelsFactory.GetItem();
                levelElement.transform.SetParent(levelsContainer, false);
                levelElement.Setup(viewModule.LocalizationSystem, $"level: {level}");

                levelElements.Add(level, levelElement);
            }

            levelElements[level].AddResearch(researchElement);
        }

        private void CleanupElements()
        {
            foreach (var levelElement in levelElements)
            {
                levelElement.Value.Cleanup();
                levelElement.Value.Close();
            }
        }

        private void SetButtonTimer(float timer)
        {
            var time = TimeSpan.FromSeconds(timer);
            var timeText = string.Format("{0:D2}:{1:D2}", time.Minutes, time.Seconds);
            moreInfoTab.UpdateButton(timeText);
        }

        private void ResearchElementOnOnResearchButtonClicked(RuntimeResearch runtimeResearch)
        {
            SetResearch(runtimeResearch);
        }

        private void OnLevelChanged(int level)
        {
            CleanupElements();
            SetupElements();
        }

        private void OnResearchCompleted(RuntimeResearch research)
        {
            CleanupElements();
            SetupElements();
            viewModule.ResearchService.OnTimerChanged -= SetButtonTimer;
            SetResearch(research);
        }
    }
}