using App.Scripts.Scenes.Gameplay.Features.Tiles.General;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Animations
{
    public interface ITileAnimator
    {
        public UniTask PlayCreationAnimation(TileVisual tile);
        public UniTask PlayRotationAnimation(TileVisual tile);
        public UniTask PlayDestroyAnimation(TileVisual tile);
        public UniTask PlayActiveAnimation(TileVisual tile);
        void Cleanup();
    }
}