using Cysharp.Threading.Tasks;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Animations
{
    public interface ITileAnimator
    {
        public void Setup(TileVisual tile);

        public UniTask PlayCreationAnimation();
        public UniTask PlayDestroyAnimation();
        public UniTask PlayActiveAnimation();
        void Cleanup();
    }
}
