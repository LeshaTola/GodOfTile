using System;
using System.Linq;
using App.Scripts.Modules.Localization;
using App.Scripts.Modules.Localization.Localizers;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;
using Toggle = UnityEngine.UI.Toggle;

namespace App.Scripts.Features.Tiles.Systems.Views.Settings
{
    public class SettingsView : SystemUI
    {
        [SerializeField] private TMPLocalizer header;

        [SerializeField] private TMPLocalizer localizationText;
        [SerializeField] private TMP_Dropdown localizationDropdown;
        
        [SerializeField] private TMPLocalizer resolutionText;
        [SerializeField] private TMP_Dropdown resolutionsDropdown;

        [SerializeField] private TMPLocalizer fullScreenText;
        [SerializeField] private Toggle fullScreenToggle;

        [SerializeField] private TMPLocalizer masterVolumeText;
        [SerializeField] private Slider masterVolumeSlider;

        [SerializeField] private TMPLocalizer musicVolumeText;
        [SerializeField] private Slider musicVolumeSlider;

        [SerializeField] private TMPLocalizer effectsVolumeText;
        [SerializeField] private Slider effectsVolumeSlider;

        private SettingsViewModule viewModule;

        public void Initialize(SettingsViewModule viewModule)
        {
            Cleanup();

            this.viewModule = viewModule;

            header.Initialize(viewModule.LocalizationSystem);
            localizationText.Initialize(viewModule.LocalizationSystem);
            resolutionText.Initialize(viewModule.LocalizationSystem);
            fullScreenText.Initialize(viewModule.LocalizationSystem);
            masterVolumeText.Initialize(viewModule.LocalizationSystem);
            musicVolumeText.Initialize(viewModule.LocalizationSystem);
            effectsVolumeText.Initialize(viewModule.LocalizationSystem);
        }

        public override void Setup()
        {
            if (viewModule == null)
            {
                return;
            }
            
            localizationDropdown.ClearOptions();
            localizationDropdown.AddOptions(viewModule.LocalizationDatabase.Languages.Keys.ToList());
            var localizationIndex = localizationDropdown.options
                .FindIndex(x => x.text.Equals(viewModule.LocalizationSystem.Language));
            localizationDropdown.value = localizationIndex;

            localizationDropdown.RefreshShownValue();
            localizationDropdown.onValueChanged.AddListener(value =>
            {
                viewModule.LocalizationSystem.ChangeLanguage(localizationDropdown.options[value].text);
            });

            resolutionsDropdown.ClearOptions();
            resolutionsDropdown.AddOptions(viewModule.ScreenService.GetStringResolutions());

            resolutionsDropdown.value = viewModule.ScreenService.ResolutionIndex;
            resolutionsDropdown.RefreshShownValue();
            resolutionsDropdown.onValueChanged.AddListener(value =>
            {
                viewModule.ScreenService.ResolutionIndex = value;
            });

            fullScreenToggle.isOn = viewModule.ScreenService.IsFullScreen;
            fullScreenToggle.onValueChanged.AddListener(value =>
            {
                viewModule.ScreenService.IsFullScreen = value;
            });

            masterVolumeSlider.value = viewModule.AudioService.MasterVolume;
            masterVolumeSlider.onValueChanged.AddListener(value =>
            {
                viewModule.AudioService.MasterVolume = value;
            });

            musicVolumeSlider.value = viewModule.AudioService.MusicVolume;
            musicVolumeSlider.onValueChanged.AddListener(value =>
            {
                viewModule.AudioService.MusicVolume= value;
            });

            effectsVolumeSlider.value = viewModule.AudioService.EffectsVolume;
            effectsVolumeSlider.onValueChanged.AddListener(value =>
            {
                viewModule.AudioService.EffectsVolume= value;
            });
            
            header.Translate();
            localizationText.Translate();
            resolutionText.Translate();
            fullScreenText.Translate();
            masterVolumeText.Translate();
            musicVolumeText.Translate();
            effectsVolumeText.Translate();
        }

        public override void Cleanup()
        {
            if (viewModule == null)
            {
                return;
            }
            viewModule.SavesProvider.SaveState();

            viewModule = null;
        }
    }
}