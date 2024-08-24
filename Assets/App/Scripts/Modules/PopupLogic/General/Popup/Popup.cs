using App.Scripts.Modules.PopupLogic.Animations.Animator;
using App.Scripts.Modules.PopupLogic.General.Controllers;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Modules.PopupLogic.General.Popup
{
    [RequireComponent(typeof(Canvas), typeof(GraphicRaycaster))]
    public abstract class Popup : MonoBehaviour, IPopup
    {
        [FoldoutGroup("General")]
        [SerializeReference]
        PopupAnimationConfig animationConfig;

        [FoldoutGroup("General")]
        [SerializeField]
        protected GraphicRaycaster raycaster;

        [FoldoutGroup("General")]
        [SerializeField]
        protected Canvas canvas;

        public IPopupController Controller { get; private set; }
        public IPopupAnimator PopupAnimator { get; private set; }
        public Canvas Canvas => canvas;
        public bool Active { get; private set; }

        public void Initialize(IPopupController controller, IPopupAnimator popupAnimator)
        {
            Controller = controller;
            PopupAnimator = popupAnimator;
            PopupAnimator.Initialize(this);
            popupAnimator.Setup(animationConfig);
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
            Active = true;
            raycaster.enabled = true;
        }

        public void Deactivate()
        {
            Active = false;
            raycaster.enabled = false;
        }
    }
}