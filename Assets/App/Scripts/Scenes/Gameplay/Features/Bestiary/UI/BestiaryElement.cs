using System;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Scenes.Gameplay.Features.Bestiary.UI
{
    public class BestiaryElement : MonoBehaviour
    {
        public event Action<TileConfig, BestiaryElement> OnElementClicked;

        [SerializeField] private Button button;
        [SerializeField] private Image image;
        [SerializeField] private GameObject selector;

        private TileConfig tileConfig;

        public void Setup(TileConfig tileConfig)
        {
            this.tileConfig = tileConfig;
            image.sprite = tileConfig.TileSprite;
            button.onClick.AddListener(() => OnElementClicked?.Invoke(tileConfig, this));
        }

        public void Cleanup()
        {
            button.onClick.RemoveAllListeners();
            tileConfig = null;
        }

        public void SetSelected(bool selected)
        {
            selector.gameObject.SetActive(selected);
        }
    }
}