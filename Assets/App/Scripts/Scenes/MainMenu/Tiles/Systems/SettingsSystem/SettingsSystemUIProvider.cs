using App.Scripts.Features.Commands;
using App.Scripts.Features.Settings.Saves;
using App.Scripts.Features.Tiles.Systems.Views;
using App.Scripts.Features.Tiles.Systems.Views.Settings;
using App.Scripts.Modules.Localization;
using App.Scripts.Modules.Resolutions;
using App.Scripts.Modules.Sounds.Services;
using App.Scripts.Scenes.Gameplay.Features.Commands.Provider;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Factories.TileSystemUI;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI;

namespace App.Scripts.Scenes.MainMenu.Tiles.Systems.ExitSystem
{
    public class SettingsSystemUIProvider : ISystemUIProvider
    {
        private readonly ISystemUIFactory systemUIFactory;
        private readonly ILocalizationSystem localizationSystem;
        private readonly IAudioService audioService;
        private readonly IScreenService screenService;
        private readonly SettingsSavesProvider savesProvider;

        public SettingsSystemUIProvider(
            ISystemUIFactory systemUIFactory,
            ILocalizationSystem localizationSystem,
            IAudioService audioService,
            IScreenService screenService,
            SettingsSavesProvider savesProvider)
        {
            this.systemUIFactory = systemUIFactory;
            this.localizationSystem = localizationSystem;
            this.audioService = audioService;
            this.screenService = screenService;
            this.savesProvider = savesProvider;
        }
        public SystemUI GetSystemUI(TileSystem tileSystem)
        {
            var systemUI = systemUIFactory.GetSystemUI<SettingsView>();
            var viewModule = new SettingsViewModule(
                audioService,
                localizationSystem,
                screenService,
                savesProvider);
            
            systemUI.Initialize(viewModule);
            return systemUI;
        }
    }
}