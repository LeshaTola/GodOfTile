using System.Threading;
using App.Scripts.Modules.PopupAndViews.Animations;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Scripts.Features.View.Animations
{
    public class SlideAnimation : IAnimation
    {
        [FoldoutGroup("Show")]
        [SerializeField] private float showAnimationTime = 0.3f;

        [FoldoutGroup("Show")]
        [SerializeField] private Vector2 showScreenPosition;

        [FoldoutGroup("Show")]
        [SerializeField] private Ease showEase = Ease.InBack;

        [FoldoutGroup("Hide")]
        [SerializeField] private float hideAnimationTime = 0.3f;

        [FoldoutGroup("Hide")]
        [SerializeField] private Vector2 hideScreenPosition;

        [FoldoutGroup("Hide")]
        [SerializeField] private Ease hideEase = Ease.OutBack;

        public async UniTask PlayShowAnimation(GameObject target, CancellationToken cancellationToken)
        {
            var rectTransform = target.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = hideScreenPosition;

            Tween tween = rectTransform.DOAnchorPos(showScreenPosition, showAnimationTime);
            tween.SetEase(showEase);
            await tween.ToUniTask(cancellationToken: cancellationToken);
        }

        public async UniTask PlayHideAnimation(GameObject target, CancellationToken cancellationToken)
        {
            var rectTransform = target.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = showScreenPosition;

            Tween tween = rectTransform.DOAnchorPos(hideScreenPosition, hideAnimationTime);
            tween.SetEase(hideEase);
            await tween.ToUniTask(cancellationToken: cancellationToken);
        }
    }
}