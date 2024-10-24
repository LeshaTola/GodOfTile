using App.Scripts.Scenes.Gameplay.Features.Commands.General;
using Zenject;

namespace App.Scripts.Scenes.Gameplay.Features.Commands.Provider
{
    public class CommandsProvider : ICommandsProvider
    {
        private DiContainer diContainer;

        public CommandsProvider(DiContainer diContainer)
        {
            this.diContainer = diContainer;
        }

        public T GetCommand<T>() where T : ICommand
        {
            return (T) diContainer.Resolve(typeof(T));
        }
    }
}