using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.App.Scripts.Features.Popups.InformationPopup.Routers
{
    public interface IInformationWidgetRouter
    {
        UniTask ShowInformationWidgetPopup(TileConfig tileConfig);
        UniTask HideInformationWidgetPopup();
        void MoveInformationWidgetPopup(Vector2 screenPosition);
    }
}
