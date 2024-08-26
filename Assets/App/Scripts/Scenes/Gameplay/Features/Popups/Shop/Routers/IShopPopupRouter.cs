using Cysharp.Threading.Tasks;

namespace App.Scripts.Scenes.Gameplay.Features.Popups.Shop.Routers
{
    public interface IShopPopupRouter
    {
        UniTask ShowShopPopup();
        UniTask HideShopPopup();
    }
}