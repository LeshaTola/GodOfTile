using Cysharp.Threading.Tasks;

namespace App.Scripts.Modules.PopupLogic.General.Controllers
{
    public interface IPopupController
    {
        UniTask HideLastPopup();
        T GetPopup<T>() where T : Popup.Popup;
        void AddActivePopup(Popup.Popup popup);
        void RemoveActivePopup(Popup.Popup popup);
    }
}