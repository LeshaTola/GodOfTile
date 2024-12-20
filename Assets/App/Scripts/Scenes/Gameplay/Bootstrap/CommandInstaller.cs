﻿using App.Scripts.Scenes.Gameplay.Features.Commands;
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
            Container
                .Bind<GoToLoadSceneState>()
                .AsSingle()
                .WithArguments("exit");
            
            Container
                .Bind<GoToPauseState>()
                .AsSingle()
                .WithArguments("pause");

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
                .WithArguments("Close");
        }

        private void BindBuyAreaCommand()
        {
            Container
                .Bind<BuyAreaCommand>()
                .AsSingle()
                .WithArguments("Buy");
        }

        private void BindGoToBuyAreaStateCommand()
        {
            Container
                .Bind<GoToBuyAreaStateCommand>()
                .AsSingle()
                .WithArguments("Buy area");
        }

        private void BindGoToResearchStateCommand()
        {
            Container
                .Bind<GoToResearchStateCommand>()
                .AsSingle()
                .WithArguments("Research");
        }

        private void BindGoToBuildingStateCommand()
        {
            Container
                .Bind<GoToBuildingStateCommand>()
                .AsSingle()
                .WithArguments("Build");
        }
    }
}