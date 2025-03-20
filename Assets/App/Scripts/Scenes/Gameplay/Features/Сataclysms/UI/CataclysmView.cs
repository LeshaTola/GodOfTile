using App.Scripts.Modules.PopupAndViews.Views;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Scenes.Gameplay.Features.Сataclysms.UI
{
    public class CataclysmView : AnimatedView
    {
        [SerializeField] private Image _cataclysmImage;
        [SerializeField] private TextMeshProUGUI _cataclysmTimer;

        public void SetCataclysmImage(Sprite sprite)
        {
            _cataclysmImage.sprite = sprite;
        }
        
        public void UpdateTimer(float timer)
        {
            int minutes = Mathf.FloorToInt(timer / 60);
            int seconds = Mathf.FloorToInt(timer % 60);
            _cataclysmTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}