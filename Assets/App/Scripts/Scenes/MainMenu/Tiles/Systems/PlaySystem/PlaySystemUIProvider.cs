using App.Scripts.Features.Commands;
using App.Scripts.Features.Tiles.Systems.Views;
using App.Scripts.Modules.Localization;
using App.Scripts.Scenes.Gameplay.Features.Commands.Provider;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Factories.TileSystemUI;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI;

namespace App.Scripts.Scenes.MainMenu.Tiles.Systems.ExitSystem
{
    public class PlaySystemUIProvider : ISystemUIProvider
    {
        private readonly ISystemUIFactory systemUIFactory;
        private readonly ILocalizationSystem localizationSystem;
        private readonly ICommandsProvider commandsProvider;

        public PlaySystemUIProvider(
            ISystemUIFactory systemUIFactory,
            ILocalizationSystem localizationSystem,
            ICommandsProvider commandsProvider)
        {
            this.systemUIFactory = systemUIFactory;
            this.localizationSystem = localizationSystem;
            this.commandsProvider = commandsProvider;
        }
        public SystemUI GetSystemUI(TileSystem tileSystem)
        {
            var systemUI = systemUIFactory.GetSystemUI<TwoButtonsSystemUI>();
            var data = (PlaySystemData)tileSystem.Data;
            var yesCommand = commandsProvider.GetCommand<ContinueGameCommand>();
            var noCommand = commandsProvider.GetCommand<NewGameCommand>();
            var viewModule = new TwoButtonsSystemUIViewModule(
                data.HeaderKey,
                yesCommand,
                noCommand,
                localizationSystem);
            
            systemUI.Initialize(viewModule);
            return systemUI;
        }
    }
}