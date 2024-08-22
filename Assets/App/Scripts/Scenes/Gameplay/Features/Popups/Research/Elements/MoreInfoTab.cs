using System;
using App.Scripts.Features.Popups.Buttons;
using App.Scripts.Modules.Localization;
using App.Scripts.Modules.Localization.Localizers;
using App.Scripts.Scenes.Gameplay.Features.Popups.InformationWidget;
using App.Scripts.Scenes.Gameplay.Features.Popups.InformationWidget.ViewModels;
using App.Scripts.Scenes.Gameplay.Features.Researches.Configs;
using App.Scripts.Scenes.Gameplay.Features.Researches.Services;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Scenes.Gameplay.Features.Popups.Research.Elements
{
    public class MoreInfoTab : MonoBehaviour
    {
        [SerializeField] private TMProLocalizer header;
        [SerializeField] private Image image;
        [SerializeField] private TMProLocalizer nameText;
        [SerializeField] private ItemInformationWidget informationWidget;
        [SerializeField] private PopupButton researchButton;

        [SerializeField]
        [FoldoutGroup("Description")]
        private TMProLocalizer descriptionHeader;

        [SerializeField]
        [FoldoutGroup("Description")]
        private TMProLocalizer description;

        private Action researchAction;

        public void Initialize(ILocalizationSystem localizationSystem,
            IInformationWidgetViewModule informationWidgetViewModule)
        {

            header.Initialize(localizationSystem);
            nameText.Initialize(localizationSystem);
            descriptionHeader.Initialize(localizationSystem);
            description.Initialize(localizationSystem);

            informationWidget.Initialize(informationWidgetViewModule);
            researchButton.Initialize(localizationSystem);

            Translate();
        }

        public void UpdateInformation(RuntimeResearch runtimeResearch, Action action = null)
        {
            var resourceConfig = runtimeResearch.ResearchConfig;

            image.sprite = resourceConfig.Sprite;
            nameText.Key = resourceConfig.Name;
            description.Key = resourceConfig.Description;

            informationWidget.UpdateInformation(resourceConfig.Cost);
            UpdateButton(runtimeResearch, action);

            Translate();
        }

        public void UpdateTimerText(TimeSpan timer)
        {
            researchButton.UpdateText(string.Format("{0:D2}:{1:D2}", 
                timer.Minutes, 
                timer.Seconds));
            researchButton.Interactable = false;
        }

        public void Translate()
        {
            header.Translate();
            nameText.Translate();

            descriptionHeader.Translate();
            description.Translate();
            researchButton.Translate();
        }

        private void UpdateButton(RuntimeResearch runtimeResearch, Action action )
        {
            if (runtimeResearch.IsCompleate)
            {
                researchButton.UpdateText("It has already been researched");
                researchButton.Interactable = false;
                return;
            }

            researchButton.Interactable = true;
            researchButton.onButtonClicked -= researchAction;
            researchAction = action;
            researchButton.onButtonClicked += researchAction;
        }
    }
}