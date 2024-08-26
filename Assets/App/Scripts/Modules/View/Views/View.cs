using Cysharp.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Modules.View.Views
{
    public class View:MonoBehaviour, IView
    {
        public UniTask Show()
        {
            gameObject.SetActive(true);
            return UniTask.CompletedTask;
        }

        public UniTask Hide()
        {
            gameObject.SetActive(false);
            return UniTask.CompletedTask;
        }
    }
}