using Cysharp.Threading.Tasks;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Animations
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
