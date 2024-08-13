using App.Scripts.Scenes.Gameplay.Features.Commands;
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
            BindGoToBuyAreaStateCommand();
            BindGoToBuildingStateCommand();
            
            BindBuyAreaCommand();
            Container
                .Bind<ClosePopupCommand>()
                .AsSingle()
                .WithArguments("close");
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
        
        private void BindGoToBuyAreaStateCommand()
        {
            Container
                .Bind<GoToBuyAreaStateCommand>()
                .AsSingle()
                .WithArguments("buy area");
        }
        
        private void BindGoToBuildingStateCommand()
        {
            Container
                .Bind<GoToBuildingStateCommand>()
                .AsSingle()
                .WithArguments("buildings");
        }
    }
}