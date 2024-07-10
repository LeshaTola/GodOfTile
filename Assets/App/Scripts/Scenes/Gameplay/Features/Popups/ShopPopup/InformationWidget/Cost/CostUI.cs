using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Shop.UI.Cost
{
    public class CostUI : MonoBehaviour
    {
        [SerializeField]
        private Image resourceImage;

        [SerializeField]
        private TextMeshProUGUI costText;

        public void UpdateUI(Sprite sprite, int cost, Color color)
        {
            resourceImage.sprite = sprite;
            costText.text = cost.ToString();
            costText.color = color;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
    }
}
