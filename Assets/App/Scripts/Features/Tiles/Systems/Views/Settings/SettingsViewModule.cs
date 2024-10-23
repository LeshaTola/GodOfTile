using App.Scripts.Features.Settings.Saves;
using App.Scripts.Modules.Localization;
using App.Scripts.Modules.Resolutions;
using App.Scripts.Modules.Sounds.Services;

namespace App.Scripts.Features.Tiles.Systems.Views.Settings
{
    public class SettingsViewModule
    {
        public SettingsSavesProvider SavesProvider { get; }
        public IAudioService AudioService { get; }
        public ILocalizationSystem LocalizationSystem { get; }
        public IScreenService ScreenService { get; }

        public SettingsViewModule(IAudioService audioService, ILocalizationSystem localizationSystem,
            IScreenService screenService , SettingsSavesProvider settingsSavesProvider)
        {
            this.SavesProvider = settingsSavesProvider;
            AudioService = audioService;
            LocalizationSystem = localizationSystem;
            ScreenService = screenService;
        }
    }
}