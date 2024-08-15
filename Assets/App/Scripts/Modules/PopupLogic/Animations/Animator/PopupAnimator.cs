﻿using System;
using App.Scripts.Modules.PopupLogic.General.Popup;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace App.Scripts.Modules.PopupLogic.Animations.Animator
{
    [Serializable]
    public class PopupAnimationConfig
    {
        public float ShowAnimationTime;
        public float HideAnimationTime;
    }

    public class PopupAnimator : IPopupAnimator
    {
        private PopupAnimationConfig config;

        private Popup popup;
        private Tween tween;

        public void Initialize(Popup popup)
        {
            this.popup = popup;
        }

        public void Setup(PopupAnimationConfig animationConfig)
        {
            config = animationConfig;
        }

        public async UniTask PlayHideAnimation()
        {
            Cleanup();

            popup.transform.localScale = Vector3.one;
            tween = popup.transform.DOScale(Vector3.zero, config.HideAnimationTime);
            tween.SetEase(Ease.InBack);
            await tween.AsyncWaitForCompletion();
        }

        public async UniTask PlayShowAnimation()
        {
            Cleanup();
            popup.transform.localScale = Vector3.zero;
            tween = popup.transform.DOScale(Vector3.one, config.ShowAnimationTime);
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