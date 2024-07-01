using Assets.App.Scripts.Features.Popups.InformationPopup.Animator;
using Cysharp.Threading.Tasks;
using Module.PopupLogic.General.Controller;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Module.PopupLogic.General.Popups
{
    [RequireComponent(typeof(Canvas), typeof(GraphicRaycaster))]
    public abstract class Popup : MonoBehaviour, IPopup
    {
        [FoldoutGroup("General")]
        [SerializeField]
        protected GraphicRaycaster raycaster;

        [FoldoutGroup("General")]
        [SerializeField]
        protected Canvas canvas;

        public IPopupController Controller { get; private set; }
        public IPopupAnimator PopupAnimator { get; private set; }
        public Canvas Canvas
        {
            get => canvas;
        }

        public void Init(IPopupController controller, IPopupAnimator popupAnimator)
        {
            Controller = controller;
            PopupAnimator = popupAnimator;
            PopupAnimator.Init(this);
        }

        public virtual async UniTask Hide()
        {
            Deactivate();

            await PopupAnimator.PlayHideAnimation();
            Controller.RemoveActivePopup(this);
            gameObject.SetActive(false);
        }

        public virtual async UniTask Show()
        {
            gameObject.SetActive(true);
            Controller.AddActivePopup(this);

            await PopupAnimator.PlayShowAnimation();
            Activate();
        }

        public void Activate()
        {
            raycaster.enabled = true;
        }

        public void Deactivate()
        {
            raycaster.enabled = false;
        }
    }
}
