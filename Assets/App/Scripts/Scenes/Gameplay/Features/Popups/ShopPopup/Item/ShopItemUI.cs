using System;
using Assets.App.Scripts.Features.Popups.InformationPopup.Routers;
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
        public event Action onBuyButtonClicked;

        [SerializeField]
        private Image image;

        [SerializeField]
        private Button button;

        private IInformationWidgetRouter informationWidgetRouter;
        private TileConfig tileConfig;

        public void Setup(TileConfig tileConfig, IInformationWidgetRouter informationWidgetRouter)
        {
            this.informationWidgetRouter = informationWidgetRouter;
            this.tileConfig = tileConfig;

            image.sprite = tileConfig.TileSprite;

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => onBuyButtonClicked?.Invoke());
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            informationWidgetRouter.MoveInformationWidgetPopup(transform.position);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("Enter");

            //informationWidgetRouter.ShowInformationWidgetPopup(tileConfig);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("Exit");
            informationWidgetRouter.HideInformationWidgetPopup();
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
