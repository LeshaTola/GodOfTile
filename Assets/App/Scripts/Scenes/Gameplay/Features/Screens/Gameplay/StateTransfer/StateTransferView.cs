using System;
using System.Collections.Generic;
using App.Scripts.Modules.Localization;
using App.Scripts.Modules.Localization.Elements.Buttons;
using App.Scripts.Modules.PopupAndViews.Views;
using App.Scripts.Modules.Sounds;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Screens.Gameplay.StateTransfer
{
    public class StateTransferView : AnimatedView
    {
        public event Action OnBuildingStateButtonClicked;
        public event Action OnBuyAreaStateButtonClicked;
        public event Action OnResearchStateButtonClicked;

        [SerializeField] private TMPLocalizedButton buildingStateButton;
        [SerializeField] private TMPLocalizedButton buyAreaStateButton;
        [SerializeField] private TMPLocalizedButton researchStateButton;

        [SerializeField] private AudioDatabase audioDatabase;
        [field: ValueDropdown(nameof(GetKeys))]
        [field: SerializeField] public string ButtonSoundKey { get; private set; }

        public List<string> GetKeys()
        {
            if (audioDatabase == null)
            {
                return null;
            }
            return audioDatabase.GetKeys();
        }
        
        public void Initialize(ILocalizationSystem localizationSystem)
        {
            buildingStateButton.Initialize(localizationSystem);
            buyAreaStateButton.Initialize(localizationSystem);
            researchStateButton.Initialize(localizationSystem);

            buildingStateButton.UpdateAction(() => OnBuildingStateButtonClicked?.Invoke());
            buyAreaStateButton.UpdateAction(() => OnBuyAreaStateButtonClicked?.Invoke());
            researchStateButton.UpdateAction(() => OnResearchStateButtonClicked?.Invoke());

            buildingStateButton.Translate();
            buyAreaStateButton.Translate();
            researchStateButton.Translate();
        }

        public void Cleanup()
        {
            buildingStateButton.Cleanup();
            buyAreaStateButton.Cleanup();
            researchStateButton.Cleanup();
        }

        public void ShowResearchButton()
        {
            researchStateButton.gameObject.SetActive(true);
        }

        public void HideResearchButton()
        {
            researchStateButton.gameObject.SetActive(false);
        }
    }
}