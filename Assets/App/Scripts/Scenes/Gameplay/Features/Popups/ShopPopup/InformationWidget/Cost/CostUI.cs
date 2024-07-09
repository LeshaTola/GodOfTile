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

        public void UpdateUI(Sprite sprite, int cost)
        {
            resourceImage.sprite = sprite;
            costText.text = cost.ToString();
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
