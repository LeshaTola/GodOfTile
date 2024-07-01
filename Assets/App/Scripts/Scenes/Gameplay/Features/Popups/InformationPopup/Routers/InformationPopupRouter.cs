using Assets.App.Scripts.Features.Popups.InformationPopup.ViewModels;
using Assets.App.Scripts.Scenes.Gameplay.Features.Commands.General;
using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using Cysharp.Threading.Tasks;
using Module.Localization;
using Module.PopupLogic.General.Controller;

namespace Assets.App.Scripts.Features.Popups.InformationPopup.Routers
{
    public class InformationPopupRouter : IInformationPopupRouter
    {
        private ILabeledCommand goToGameplayStateCommand;
        private ILocalizationSystem localizationSystem;
        private IPopupController popupController;

        private InformationPopup popup;

        public InformationPopupRouter(
            ILabeledCommand goToGameplayStateCommand,
            ILocalizationSystem localizationSystem,
            IPopupController popupController
        )
        {
            this.goToGameplayStateCommand = goToGameplayStateCommand;
            this.localizationSystem = localizationSystem;
            this.popupController = popupController;
        }

        public async UniTask HideInformationPopup()
        {
            if (popup == null)
            {
                return;
            }

            await popup.Hide();
            popup = null;
        }

        public async UniTask ShowInformationPopup(TileConfig tileConfig)
        {
            popup = popupController.GetPopup<InformationPopup>();

            var viewModule = new InformationViewModule(
                localizationSystem,
                goToGameplayStateCommand,
                tileConfig
            );

            popup.Setup(viewModule);
            await popup.Show();
        }
    }
}
