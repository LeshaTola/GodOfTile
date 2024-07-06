using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Inventory.UI
{
    public class ResourceUI : MonoBehaviour, IResourceUI
    {
        [SerializeField]
        private TextMeshProUGUI amountText;

        [SerializeField]
        private Image image;

        public void Initialize(Sprite sprite, int amount)
        {
            image.sprite = sprite;
            UpdateAmount(amount);
        }

        public void UpdateAmount(int amount)
        {
            amountText.text = amount.ToString();
        }
    }
}
