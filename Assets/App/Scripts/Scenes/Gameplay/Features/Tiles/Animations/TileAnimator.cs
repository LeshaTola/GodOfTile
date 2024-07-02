using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Animations
{
	public class TileAnimator : ITileAnimator
	{
		private TileAnimationConfig config;

		private Tween tween;

		public TileAnimator(TileAnimationConfig config)
		{
			this.config = config;
		}

		public async UniTask PlayActiveAnimation(TileVisual tile)
		{
			Cleanup();
			var targetScale = tile.transform.localScale;
			tile.transform.localScale = targetScale * 0.9f;

			tween = tile
				.transform.DOScale(targetScale, config.CreateAnimationDuration)
				.SetLoops(-1, LoopType.Yoyo);
			tween.SetEase(Ease.OutBack);

			await tween.AsyncWaitForCompletion();
		}

		public async UniTask PlayCreationAnimation(TileVisual tile)
		{
			Cleanup();
			var targetScale = tile.transform.localScale;
			tile.transform.localScale = Vector3.zero;

			tween = tile.transform.DOScale(targetScale, config.CreateAnimationDuration);

			tween.SetEase(Ease.OutBack);
			await tween.AsyncWaitForCompletion();
		}

		public async UniTask PlayDestroyAnimation(TileVisual tile)
		{
			Cleanup();

			tween = tile.transform.DOScale(Vector3.zero, config.CreateAnimationDuration);

			tween.SetEase(Ease.InBack);
			await tween.AsyncWaitForCompletion();
		}

		public void Cleanup()
		{
			if (tween != null && tween.IsActive())
			{
				tween.Complete();
				tween.Kill();
			}
		}
	}
}
