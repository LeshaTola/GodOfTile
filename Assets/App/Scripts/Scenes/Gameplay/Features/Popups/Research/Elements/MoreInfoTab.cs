using System;
using App.Scripts.Features.Popups.Buttons;
using App.Scripts.Modules.Localization;
using App.Scripts.Modules.Localization.Localizers;
using App.Scripts.Scenes.Gameplay.Features.Inventory.Configs;
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
            researchButton.UpdateText("buy");
            researchButton.UpdateText(text);
            Translate();
            
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
            researchButton.Translate();
        }
    }
}