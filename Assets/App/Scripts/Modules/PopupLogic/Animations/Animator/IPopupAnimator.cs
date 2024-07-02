using Cysharp.Threading.Tasks;
using Module.PopupLogic.General.Popups;

namespace Assets.App.Scripts.Features.Popups.InformationPopup.Animator
{
	public interface IPopupAnimator
	{
		void Cleanup();
		void Setup(PopupAnimationConfig animationConfig);
		void Init(Popup popup);
		UniTask PlayHideAnimation();
		UniTask PlayShowAnimation();
	}
}
