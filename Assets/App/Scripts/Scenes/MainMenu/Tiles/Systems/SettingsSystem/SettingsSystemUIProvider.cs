using App.Scripts.Features.Commands;
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
        private readonly ICommandsProvider commandsProvider;
        private readonly IAudioService audioService;
        private readonly IScreenService screenService;

        public SettingsSystemUIProvider(
            ISystemUIFactory systemUIFactory,
            ILocalizationSystem localizationSystem,
            ICommandsProvider commandsProvider,
            IAudioService audioService,
            IScreenService screenService)
        {
            this.systemUIFactory = systemUIFactory;
            this.localizationSystem = localizationSystem;
            this.commandsProvider = commandsProvider;
            this.audioService = audioService;
            this.screenService = screenService;
        }
        public SystemUI GetSystemUI(TileSystem tileSystem)
        {
            var systemUI = systemUIFactory.GetSystemUI<SettingsView>();
            var viewModule = new SettingsViewModule(
                audioService,
                localizationSystem,
                screenService);
            
            systemUI.Initialize(viewModule);
            return systemUI;
        }
    }
}