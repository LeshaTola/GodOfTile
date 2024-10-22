using App.Scripts.Modules.Resolutions;
using App.Scripts.Modules.Saves;
using App.Scripts.Modules.Sounds.Services;

namespace App.Scripts.Features.Settings.Saves
{
    public class SettingsSavesProvider: ISavable
    {
        private readonly IDataProvider<SettingsData> dataProvider;
        private readonly IAudioService audioService;
        private readonly IScreenService screenService;

        public SettingsSavesProvider(
            IDataProvider<SettingsData> dataProvider,
            IAudioService audioService,
            IScreenService screenService)
        {
            this.dataProvider = dataProvider;
            this.audioService = audioService;
            this.screenService = screenService;
        }

        public void SaveState()
        {
            dataProvider.SaveData(new SettingsData()
            {
                MasterVolume = audioService.MasterVolume,
                MusicVolume = audioService.MusicVolume,
                EffectsVolume = audioService.EffectsVolume,
                
                IsFullScreen = screenService.IsFullScreen,
                ResolutionIndex = screenService.ResolutionIndex,
            });
        }

        public void LoadState()
        {
            if (!dataProvider.HasData())
            {
                return;
                CreateState();
            }

            var data = dataProvider.GetData();
            audioService.MasterVolume = data.MasterVolume;
            audioService.MusicVolume = data.MusicVolume;
            audioService.EffectsVolume = data.EffectsVolume;

            screenService.IsFullScreen = data.IsFullScreen;
            screenService.ResolutionIndex = data.ResolutionIndex;
        }

        private void CreateState()
        {
            dataProvider.SaveData(new SettingsData()
            {
                MasterVolume = 1,
                MusicVolume = 1,
                EffectsVolume = 1,
                
                IsFullScreen = true,
                ResolutionIndex = 0,
            });
        }
    }
}