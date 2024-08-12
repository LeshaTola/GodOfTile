using App.Scripts.Scenes.Gameplay.Features.Commands.BuyAreaCommand;
using App.Scripts.Scenes.Gameplay.Features.Commands.GoToGamePlayStateCommands;
using Zenject;

namespace App.Scripts.Scenes.Gameplay.Bootstrap
{
    public class CommandInstaller : Installer<CommandInstaller>
    {
        public override void InstallBindings()
        {
            BindGoToGameplayStateCommand();
            BindBuyAreaCommand();
        }

        private void BindGoToGameplayStateCommand()
        {
            Container
                .Bind<GoToGamePlayStateCommand>()
                .AsSingle()
                .WithArguments("close");
        }
        
        private void BindBuyAreaCommand()
        {
            Container
                .Bind<BuyAreaCommand>()
                .AsSingle()
                .WithArguments("buy");
        }
    }
}