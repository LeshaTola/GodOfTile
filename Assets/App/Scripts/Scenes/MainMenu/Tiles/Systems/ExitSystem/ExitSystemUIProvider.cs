using App.Scripts.Features.Commands;
using App.Scripts.Features.Tiles.Systems.Views;
using App.Scripts.Modules.Localization;
using App.Scripts.Scenes.Gameplay.Features.Commands.Provider;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Factories.TileSystemUI;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI;

namespace App.Scripts.Scenes.MainMenu.Tiles.Systems.ExitSystem
{
    public class ExitSystemUIProvider : ISystemUIProvider
    {
        private readonly ISystemUIFactory systemUIFactory;
        private readonly ILocalizationSystem localizationSystem;
        private readonly ICommandsProvider commandsProvider;

        public ExitSystemUIProvider(
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
            var data = (ExitSystemData)tileSystem.Data;
            var yesCommand = commandsProvider.GetCommand<ExitGameCommand>();
            var noCommand = commandsProvider.GetCommand<ExitGameCommand>();
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