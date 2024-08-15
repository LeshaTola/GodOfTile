using App.Scripts.Modules.Localization;
using App.Scripts.Modules.PopupLogic.General.Controllers;
using App.Scripts.Scenes.Gameplay.Features.Commands.General;
using App.Scripts.Scenes.Gameplay.Features.Commands.GoToStateCommands;
using App.Scripts.Scenes.Gameplay.Features.Popups.InformationPopup.ViewModels;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Scenes.Gameplay.Features.Popups.InformationPopup.Routers
{
    public class InformationPopupRouter : IInformationPopupRouter
    {
        private ILabeledCommand goToGameplayStateCommand;
        private ILocalizationSystem localizationSystem;
        private IPopupController popupController;

        private InformationPopup popup;

        public InformationPopupRouter(
            GoToGamePlayStateCommand goToGameplayStateCommand,
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
            if (popup == null)
            {
                popup = popupController.GetPopup<InformationPopup>();
            }

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