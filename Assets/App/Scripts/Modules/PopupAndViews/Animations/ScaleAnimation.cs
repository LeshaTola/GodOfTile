using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Scripts.Modules.PopupAndViews.Animations
{
    public class ScaleAnimation: IAnimation
    {
        [FoldoutGroup("Show")]
        [SerializeField] protected float showAnimationTime = 0.3f;
        
        [FoldoutGroup("Show")]
        [SerializeField] protected Ease showEase = Ease.OutBack;

        [FoldoutGroup("Hide")]
        [SerializeField] protected float hideAnimationTime= 0.3f;

        [FoldoutGroup("Hide")]
        [SerializeField] protected Ease hideEase = Ease.InBack;

        public virtual async UniTask PlayShowAnimation(GameObject target, CancellationToken cancellationToken)
        {
            target.transform.localScale = Vector3.zero;
            Tween tween = target.transform.DOScale(1, showAnimationTime);

            await tween.SetEase(showEase).ToUniTask(cancellationToken: cancellationToken);
        }

        public virtual async UniTask PlayHideAnimation(GameObject target, CancellationToken cancellationToken)
        {
            target.transform.localScale = Vector3.one;
            Tween tween = target.transform.DOScale(0, hideAnimationTime);
            await tween.SetEase(hideEase).ToUniTask(cancellationToken: cancellationToken);
        }
    }
}