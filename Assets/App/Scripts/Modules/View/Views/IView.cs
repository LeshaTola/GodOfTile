using Cysharp.Threading.Tasks;

namespace App.Scripts.Modules.View.Views
{
    public interface IView
    {
        public UniTask Show();
        public UniTask Hide();
    }
}