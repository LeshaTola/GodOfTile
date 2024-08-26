using System;
using App.Scripts.Modules.Localization;
using App.Scripts.Modules.Localization.Elements.Buttons;
using App.Scripts.Modules.View.Views;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Screens.Gameplay.StateTransitioner
{
    public class StateTransferView : View
    {
        public event Action OnBuildingStateButtonClicked;
        public event Action OnBuyAreaStateButtonClicked;
        public event Action OnResearchStateButtonClicked;

        [SerializeField] private TMPLocalizedButton buildingStateButton;
        [SerializeField] private TMPLocalizedButton buyAreaStateButton;
        [SerializeField] private TMPLocalizedButton researchStateButton;
        
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