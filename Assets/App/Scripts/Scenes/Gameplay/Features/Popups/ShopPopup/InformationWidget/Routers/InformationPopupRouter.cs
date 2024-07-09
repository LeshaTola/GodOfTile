using Assets.App.Scripts.Scenes.Gameplay.Features.Shop.Factories.Cost;
using Assets.App.Scripts.Scenes.Gameplay.Features.Shop.UI.Information;
using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using Cysharp.Threading.Tasks;
using Module.Localization;
using Module.PopupLogic.General.Controller;
using UnityEngine;

namespace Assets.App.Scripts.Features.Popups.InformationPopup.Routers
{
    public class InformationWidgetRouter : IInformationWidgetRouter
    {
        private ICostUIFactory costUIFactory;
        private ILocalizationSystem localizationSystem;
        private IPopupController popupController;

        private InformationWidgetPopup popup;

        public InformationWidgetRouter(
            ILocalizationSystem localizationSystem,
            IPopupController popupController,
            ICostUIFactory costUIFactory
        )
        {
            this.localizationSystem = localizationSystem;
            this.popupController = popupController;
            this.costUIFactory = costUIFactory;
        }

        public async UniTask HideInformationWidgetPopup()
        {
            if (popup == null)
            {
                return;
            }

            await popup.Hide();
            popup = null;
        }

        public void MoveInformationWidgetPopup(Vector2 screenPosition)
        {
            if (popup == null)
            {
                return;
            }

            popup.transform.position = screenPosition;

            /*RectTransformUtility.ScreenPointToWorldPointInRectangle(
                popup.transform.parent.GetComponent<RectTransform>(),
                screenPosition,
                Camera.main,
                out var worldPoint
            );
            popup.GetComponent<RectTransform>().anchoredPosition = worldPoint;*/
        }

        public async UniTask ShowInformationWidgetPopup(TileConfig tileConfig)
        {
            if (popup == null)
            {
                popup = popupController.GetPopup<InformationWidgetPopup>();
            }

            var viewModule = new InformationWidgetViewModule(
                localizationSystem,
                costUIFactory,
                tileConfig
            );
            popup.Setup(viewModule);

            await popup.Show();
        }
    }
}
