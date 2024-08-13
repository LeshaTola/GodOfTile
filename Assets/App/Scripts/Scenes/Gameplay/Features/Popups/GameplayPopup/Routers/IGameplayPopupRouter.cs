using Cysharp.Threading.Tasks;

namespace App.Scripts.Scenes.Gameplay.StateMachines.State.UI.Routers
{
    public interface IGameplayPopupRouter
    {
        UniTask Show();
        UniTask Hide();
    }
}