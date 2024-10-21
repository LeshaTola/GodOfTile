using System;
using App.Scripts.Modules.Localization.Localizers;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Features.Tiles.Systems.Views.Settings
{
    public class SettingsView : SystemUI
    {
        [SerializeField] private TMPLocalizer header;

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

            resolutionsDropdown.ClearOptions();
            resolutionsDropdown.AddOptions(viewModule.ScreenService.GetStringResolutions());

            resolutionsDropdown.onValueChanged.AddListener(value =>
            {
                var texts = resolutionsDropdown.options[value].text.Split("x");
                var width = Int32.Parse(texts[0]);
                var height = Int32.Parse(texts[1]);
                viewModule.ScreenService.SetResolution(width, height);
            });

            fullScreenToggle.onValueChanged.AddListener(value =>
            {
                viewModule.ScreenService.ChangeFullScreen(value);
            });

            masterVolumeSlider.onValueChanged.AddListener(value =>
            {
                viewModule.AudioService.ChangeMasterVolume(value);
            });

            musicVolumeSlider.onValueChanged.AddListener(value =>
            {
                viewModule.AudioService.ChangeMusicVolume(value);
            });

            effectsVolumeSlider.onValueChanged.AddListener(value =>
            {
                viewModule.AudioService.ChangeEffectsVolume(value);
            });
        }

        public override void Cleanup()
        {
            if (viewModule == null)
            {
                return;
            }
        }
    }
}