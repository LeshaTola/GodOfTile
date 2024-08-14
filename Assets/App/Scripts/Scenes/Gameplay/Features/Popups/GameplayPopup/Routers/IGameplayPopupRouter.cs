using Cysharp.Threading.Tasks;

namespace App.Scripts.Scenes.Gameplay.Features.Popups.GameplayPopup.Routers
{
    public interface IGameplayPopupRouter
    {
        UniTask Show();
        UniTask Hide();
    }
}