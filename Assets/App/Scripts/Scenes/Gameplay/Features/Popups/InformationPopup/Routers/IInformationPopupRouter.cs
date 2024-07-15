using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using Cysharp.Threading.Tasks;

namespace Assets.App.Scripts.Features.Popups.InformationPopup.Routers
{
    public interface IInformationPopupRouter
    {
        UniTask ShowInformationPopup(TileConfig tileConfig);
        UniTask HideInformationPopup();
    }
}