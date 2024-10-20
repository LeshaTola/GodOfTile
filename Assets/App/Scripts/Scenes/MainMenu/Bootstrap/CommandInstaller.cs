using App.Scripts.Features.Commands;
using App.Scripts.Scenes.Gameplay.Features.Commands;
using App.Scripts.Scenes.Gameplay.Features.Commands.BuyAreaCommand;
using App.Scripts.Scenes.Gameplay.Features.Commands.GoToStateCommands;
using App.Scripts.Scenes.Gameplay.Features.Commands.Provider;
using Zenject;

namespace App.Scripts.Scenes.MainMenu.Bootstrap
{
    public class CommandInstaller : Installer<CommandInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ICommandsProvider>().To<CommandsProvider>().AsSingle();
            
            Container
                .Bind<NewGameCommand>()
                .AsSingle()
                .WithArguments("new game");
            
            Container
                .Bind<ContinueGameCommand>()
                .AsSingle()
                .WithArguments("continue");
            
            Container
                .Bind<ExitGameCommand>()
                .AsSingle()
                .WithArguments("exit");
            
            
        }

        
    }
}