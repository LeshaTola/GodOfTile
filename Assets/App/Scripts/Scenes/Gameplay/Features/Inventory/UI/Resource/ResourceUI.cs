using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Scenes.Gameplay.Features.Inventory.UI.Resource
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