using Cysharp.Threading.Tasks;
using DG.Tweening;
using Module.PopupLogic.General.Popups;
using UnityEngine;

namespace Assets.App.Scripts.Features.Popups.InformationPopup.Animator
{
    public class PopupAnimationConfig
    {
        public float ShowAnimationTime;
        public float HideAnimationTime;
    }

    public class PopupAnimator : IPopupAnimator
    {
        private Popup popup;
        private PopupAnimationConfig config;
        private Tween tween;

        public void Init(Popup popup)
        {
            this.popup = popup;
        }

        public async UniTask PlayHideAnimation()
        {
            Cleanup();
            tween = popup.transform.DOScale(Vector3.zero, config.HideAnimationTime);
            tween.SetEase(Ease.OutBack);
            await tween.AsyncWaitForCompletion();
        }

        public async UniTask PlayShowAnimation()
        {
            Cleanup();
            popup.transform.DOScale(Vector3.one, config.ShowAnimationTime);
            tween.SetEase(Ease.OutBack);
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
