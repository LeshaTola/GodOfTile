using System;
using App.Scripts.Features.Popups.Buttons;
using App.Scripts.Modules.Localization;
using App.Scripts.Modules.Localization.Localizers;
using App.Scripts.Scenes.Gameplay.Features.Researches.Configs;
using App.Scripts.Scenes.Gameplay.Features.Screens.CostWidget;
using App.Scripts.Scenes.Gameplay.Features.Screens.CostWidget.ViewModels;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Scenes.Gameplay.Features.Popups.Research.Elements
{
    public class MoreInfoTab : MonoBehaviour
    {
        [SerializeField] private TMPLocalizer header;
        [SerializeField] private Image image;
        [SerializeField] private TMPLocalizer nameText;
        [SerializeField] private ItemInformationWidget informationWidget;
        [SerializeField] private PopupButton researchButton;

        [SerializeField]
        [FoldoutGroup("Description")]
        private TMPLocalizer descriptionHeader;

        [SerializeField]
        [FoldoutGroup("Description")]
        private TMPLocalizer description;

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

        public void UpdateInformation(ResearchConfig researchConfig)
        {
            image.sprite = researchConfig.Sprite;
            nameText.Key = researchConfig.Name;
            description.Key = researchConfig.Description;
            informationWidget.UpdateInformation(researchConfig.Cost);

            Translate();
        }

        public void UpdateButton(string text, Action action = null)
        {
            researchButton.UpdateText(text);
            researchButton.Translate();

            if (action == null)
            {
                researchButton.Interactable = false;
                return;
            }

            researchButton.Interactable = true;
            researchButton.onButtonClicked -= researchAction;
            researchAction = action;
            researchButton.onButtonClicked += researchAction;
        }

        public void Translate()
        {
            header.Translate();
            nameText.Translate();

            descriptionHeader.Translate();
            description.Translate();
            informationWidget.Translate();
        }
    }
}