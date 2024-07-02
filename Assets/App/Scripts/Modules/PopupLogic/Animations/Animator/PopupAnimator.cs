using Cysharp.Threading.Tasks;
using DG.Tweening;
using Module.PopupLogic.General.Popups;
using System;
using UnityEngine;

namespace Assets.App.Scripts.Features.Popups.InformationPopup.Animator
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

		public void Init(Popup popup)
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
