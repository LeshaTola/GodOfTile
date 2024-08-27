using System.Threading;
using App.Scripts.Modules.PopupAndViews.Animations;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Scripts.Modules.PopupAndViews.Views
{
    public class AnimatedView : SerializedMonoBehaviour
    {
        [SerializeField] private IAnimation animation;

        private UniTask animationTask;
        private CancellationTokenSource cancellationTokenSource;

        public async UniTask Show()
        {
            Cleanup();

            cancellationTokenSource = new CancellationTokenSource();
            animationTask = animation.PlayShowAnimation(gameObject, cancellationTokenSource.Token);

            gameObject.SetActive(true);
            await animationTask;
        }

        public async UniTask Hide()
        {
            Cleanup();

            cancellationTokenSource = new CancellationTokenSource();
            animationTask = animation.PlayHideAnimation(gameObject, cancellationTokenSource.Token);

            await animationTask;
            gameObject.SetActive(false);
        }

        private void Cleanup()
        {
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
                cancellationTokenSource.Dispose();
                cancellationTokenSource = null;
            }
        }
    }
}