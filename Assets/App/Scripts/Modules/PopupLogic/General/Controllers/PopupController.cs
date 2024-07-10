using System;
using System.Collections.Generic;
using System.Linq;
using Assets.App.Scripts.Features.Popups.InformationPopup.Animator;
using Cysharp.Threading.Tasks;
using Module.PopupLogic.General.Popups;
using Module.PopupLogic.General.Providers;
using UnityEngine.UI;

namespace Module.PopupLogic.General.Controller
{
    public class PopupController : IPopupController
    {
        private IPopupProvider popupProvider;
        private Image screenBlocker;
        private List<Popup> currentPopups;
        private IPopupAnimator popupAnimator;

        public PopupController(
            IPopupProvider popupProvider,
            Image screenBlocker,
            IPopupAnimator popupAnimator
        )
        {
            this.popupProvider = popupProvider;
            this.screenBlocker = screenBlocker;
            currentPopups = new();
            this.popupAnimator = popupAnimator;
        }

        public void AddActivePopup(Popup popup)
        {
            DeactivatePrevPopup();
            popup.Canvas.sortingLayerName = "Popup";
            popup.Canvas.sortingOrder = currentPopups.Count + 1;
            currentPopups.Add(popup);
        }

        public void RemoveActivePopup(Popup popup)
        {
            if (currentPopups.Count <= 0)
            {
                return;
            }

            if (currentPopups.Last() == popup)
            {
                ActivatePrevPopup();
            }

            currentPopups.Remove(popup);
            popupProvider.PopupPoolsDictionary[popup.GetType()].Release(popup);
        }

        public T GetPopup<T>()
            where T : Popup
        {
            Type type = typeof(T);
            var popup = popupProvider.PopupPoolsDictionary[type].Get();
            popup.Init(this, popupAnimator);
            return (T)popup;
        }

        public async UniTask HideLastPopup()
        {
            if (currentPopups.Count <= 0)
            {
                return;
            }

            var popup = currentPopups.Last();
            await popup.Hide();
        }

        private void DeactivatePrevPopup()
        {
            if (currentPopups.Count > 0)
            {
                currentPopups.Last().Deactivate();
                return;
            }
            //screenBlocker.gameObject.SetActive(true);
        }

        private void ActivatePrevPopup()
        {
            if (currentPopups.Count > 1)
            {
                currentPopups[currentPopups.Count - 2].Activate();
                return;
            }
            screenBlocker.gameObject.SetActive(false);
        }
    }
}
