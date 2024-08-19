using App.Scripts.Scenes.Gameplay.Features.Commands;
using App.Scripts.Scenes.Gameplay.Features.Commands.BuyAreaCommand;
using App.Scripts.Scenes.Gameplay.Features.Commands.GoToStateCommands;
using App.Scripts.Scenes.Gameplay.Features.Commands.Provider;
using Zenject;

namespace App.Scripts.Scenes.Gameplay.Bootstrap
{
    public class CommandInstaller : Installer<CommandInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<ICommandsProvider>().To<CommandsProvider>().AsSingle();
            
            BindGoToGameplayStateCommand();
            BindGoToBuyAreaStateCommand();
            BindGoToBuildingStateCommand();
            BindGoToResearchStateCommand();
            
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
        
        private void BindGoToResearchStateCommand()
        {
            Container
                .Bind<GoToResearchStateCommand>()
                .AsSingle()
                .WithArguments("research");
        }
        
        private void BindGoToBuildingStateCommand()
        {
            Container
                .Bind<GoToBuildingStateCommand>()
                .AsSingle()
                .WithArguments("build");
        }
    }
}