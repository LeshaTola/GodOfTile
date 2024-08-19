using App.Scripts.Scenes.Gameplay.Features.Commands.General;

namespace App.Scripts.Scenes.Gameplay.Features.Commands.Provider
{
    public interface ICommandsProvider
    {
        T GetCommand<T>() where T : ICommand;
    }
}