using Cysharp.Threading.Tasks;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Popups.ShopPopup.Routers
{
    public interface IShopPopupRouter
    {
        UniTask ShowShopPopup();
        UniTask HideShopPopup();
    }
}
