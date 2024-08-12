using App.Scripts.Modules.PopupLogic.General.Popup;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Modules.PopupLogic.Animations.Animator
{
    public interface IPopupAnimator
    {
        void Cleanup();
        void Setup(PopupAnimationConfig animationConfig);
        void Initialize(Popup popup);
        UniTask PlayHideAnimation();
        UniTask PlayShowAnimation();
    }
}