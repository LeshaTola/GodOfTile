using App.Scripts.Modules.PopupLogic.Animations.Animator;
using App.Scripts.Modules.PopupLogic.General.Controllers;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Modules.PopupLogic.General.Popup
{
    public interface IPopup
    {
        public IPopupController Controller { get; }

        public UniTask Show();
        public UniTask Hide();
        public void Activate();
        public void Deactivate();
        void Init(IPopupController controller, IPopupAnimator popupAnimator);
    }
}