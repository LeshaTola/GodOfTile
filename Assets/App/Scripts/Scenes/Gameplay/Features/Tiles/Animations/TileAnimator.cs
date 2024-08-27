using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.General;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Animations
{
    public class TileAnimator : ITileAnimator
    {
        private TileAnimationConfig config;

        private Tween tween;

        public TileAnimator(TileAnimationConfig config)
        {
            this.config = config;
        }

        public async UniTask PlayRotationAnimation(TileVisual tile)
        {
            Cleanup();

            var newRotation = new Vector3(0f, tile.transform.rotation.eulerAngles.y + 90f, 0f);
            tween = tile.transform.DORotate(
                newRotation,
                config.RotationAnimationDuration,
                RotateMode.FastBeyond360
            );
            await tween.SetEase(Ease.InOutBack);
        }

        public async UniTask PlayActiveAnimation(TileVisual tile)
        {
            Cleanup();

            var targetScale = tile.transform.localScale;
            tile.transform.localScale = targetScale * 0.9f;

            tween = tile
                .transform.DOScale(targetScale, config.CreateAnimationDuration)
                .SetLoops(-1, LoopType.Yoyo);
            await tween.SetEase(Ease.OutBack);
        }

        public async UniTask PlayCreationAnimation(TileVisual tile)
        {
            Cleanup();

            var targetScale = tile.transform.localScale;
            tile.transform.localScale = Vector3.zero;

            tween = tile.transform.DOScale(targetScale, config.CreateAnimationDuration);
            await tween.SetEase(Ease.OutBack);
        }

        public async UniTask PlayDestroyAnimation(TileVisual tile)
        {
            Cleanup();

            tween = tile.transform.DOScale(Vector3.zero, config.CreateAnimationDuration);
            await tween.SetEase(Ease.InBack);
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