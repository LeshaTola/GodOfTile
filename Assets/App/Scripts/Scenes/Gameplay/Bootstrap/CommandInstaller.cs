using App.Scripts.Scenes.Gameplay.Features.Commands.General;
using App.Scripts.Scenes.Gameplay.Features.Commands.GoToGamePlayStateCommands;
using Zenject;

namespace App.Scripts.Scenes.Gameplay.Bootstrap
{
    public class CommandInstaller : Installer<CommandInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<ILabeledCommand>()
                .To<GoToGamePlayStateCommand>()
                .AsSingle()
                .WithArguments("close");
        }
    }
}