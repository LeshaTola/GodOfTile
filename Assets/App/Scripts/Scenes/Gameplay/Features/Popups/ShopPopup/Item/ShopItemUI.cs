using System;
using Assets.App.Scripts.Scenes.Gameplay.Features.Shop.UI.Information;
using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Shop.UI.Item
{
    public class ShopItemUI
        : MonoBehaviour,
            IPointerEnterHandler,
            IPointerExitHandler,
            IPointerMoveHandler
    {
        public event Action<TileConfig> onBuyButtonClicked;

        [SerializeField]
        private Image image;

        [SerializeField]
        private Button button;

        private ItemInformationWidget informationWidget;
        private TileConfig tileConfig;

        public void Setup(TileConfig tileConfig, ItemInformationWidget informationWidget)
        {
            this.informationWidget = informationWidget;
            this.tileConfig = tileConfig;

            image.sprite = tileConfig.TileSprite;

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => onBuyButtonClicked?.Invoke(tileConfig));
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            informationWidget.transform.position = transform.position;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            informationWidget.UpdateInformation(tileConfig);
            informationWidget.Show();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            informationWidget.Hide();
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