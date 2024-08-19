using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Scenes.Gameplay.Features.Popups.Information.Routers
{
    public interface IInformationPopupRouter
    {
        UniTask ShowInformationPopup(TileConfig tileConfig);
        UniTask HideInformationPopup();
    }
}