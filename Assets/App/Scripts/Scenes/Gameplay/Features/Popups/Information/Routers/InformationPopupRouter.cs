using App.Scripts.Modules.Localization;
using App.Scripts.Modules.PopupLogic.General.Controllers;
using App.Scripts.Scenes.Gameplay.Features.Commands.General;
using App.Scripts.Scenes.Gameplay.Features.Commands.GoToStateCommands;
using App.Scripts.Scenes.Gameplay.Features.Popups.Information.ViewModels;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Factories.TileSystemUIProvider;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Scenes.Gameplay.Features.Popups.Information.Routers
{
    public class InformationPopupRouter : IInformationPopupRouter
    {
        private ITileSystemUIProvidersFactory tileSystemUIProvidersFactory;
        private ILabeledCommand goToGameplayStateCommand;
        private ILocalizationSystem localizationSystem;
        private IPopupController popupController;

        private InformationPopup popup;

        public InformationPopupRouter(
            ITileSystemUIProvidersFactory tileSystemUIProvidersFactory,
            GoToGamePlayStateCommand goToGameplayStateCommand,
            ILocalizationSystem localizationSystem,
            IPopupController popupController
        )
        {
            this.tileSystemUIProvidersFactory = tileSystemUIProvidersFactory;
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
                tileSystemUIProvidersFactory,
                localizationSystem,
                goToGameplayStateCommand,
                tileConfig
            );
            popup.Setup(viewModule);

            await popup.Show();
        }
    }
}