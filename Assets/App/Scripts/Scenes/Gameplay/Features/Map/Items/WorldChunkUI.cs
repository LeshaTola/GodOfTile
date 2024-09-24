using System;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Scenes.Gameplay.Features.Map.Items
{
    public class WorldChunkUI : MonoBehaviour
    {
        public event Action OnBuyButtonClicked;

        [SerializeField] private Button buyButton;

        public void Initialize()
        {
            buyButton.onClick.RemoveAllListeners();
            buyButton.onClick.AddListener(() => OnBuyButtonClicked?.Invoke());
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}