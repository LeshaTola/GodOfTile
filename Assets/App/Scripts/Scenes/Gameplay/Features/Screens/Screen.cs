using Cysharp.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Screens
{
    public abstract class Screen : MonoBehaviour
    {
        //[SerializeField] private PopupAnimationConfig popupAnimationConfig;
        //private PopupAnimator popupAnimator;

        public virtual UniTask Show()
        {
            gameObject.SetActive(true);
            return UniTask.CompletedTask;
        }

        public virtual UniTask Hide()
        {
            gameObject.SetActive(false);
            return UniTask.CompletedTask;
        }
    }
}