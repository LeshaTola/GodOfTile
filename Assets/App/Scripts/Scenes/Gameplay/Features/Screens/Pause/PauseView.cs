using System;
using System.Collections.Generic;
using App.Scripts.Modules.Localization;
using App.Scripts.Modules.Localization.Elements.Buttons;
using App.Scripts.Modules.Localization.Localizers;
using App.Scripts.Modules.PopupAndViews.Views;
using App.Scripts.Modules.Sounds;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Screens.Pause
{
    public class PauseView:AnimatedView
    {
        public event Action OnContinueButtonClicked;
        public event Action OnSettingsButtonClicked;
        public event Action OnExitButtonClicked;
        
        [SerializeField] private TMPLocalizer header;
        [SerializeField] private TMPLocalizedButton continueButton;
        [SerializeField] private TMPLocalizedButton settingsButton;
        [SerializeField] private TMPLocalizedButton exitButton;
        
        [SerializeField] private AudioDatabase audioDatabase;
        [field: ValueDropdown(nameof(GetKeys))]
        [field: SerializeField] public string ButtonSoundKey { get; private set; }

        private List<string> GetKeys()
        {
            if (audioDatabase == null)
            {
                return null;
            }
            return audioDatabase.GetKeys();
        }
        
        public void Initialize(ILocalizationSystem localizationSystem)
        {
            header.Initialize(localizationSystem);
            continueButton.Initialize(localizationSystem);
            settingsButton.Initialize(localizationSystem);
            exitButton.Initialize(localizationSystem);
            
            continueButton.UpdateAction(() => OnContinueButtonClicked?.Invoke());
            settingsButton.UpdateAction(() => OnSettingsButtonClicked?.Invoke());
            exitButton.UpdateAction(() => OnExitButtonClicked?.Invoke());
            
            header.Translate();
            continueButton.Translate();
            settingsButton.Translate();
            exitButton.Translate();
        }
        
        public void Cleanup()
        {
            continueButton.Cleanup();
            settingsButton.Cleanup();
            exitButton.Cleanup();
        }
    }
}