using System.Threading;
using App.Scripts.Modules.Localization;
using App.Scripts.Modules.PopupAndViews.General.Controllers;
using App.Scripts.Scenes.Gameplay.Features.Commands;
using App.Scripts.Scenes.Gameplay.Features.Popups.Information.ViewModels;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Factories.TileSystemUIProvider;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Scenes.Gameplay.Features.Popups.Information.Routers
{
    public class InformationPopupRouter : IInformationPopupRouter
    {
        private ITileSystemUIProvidersFactory tileSystemUIProvidersFactory;
        private ILocalizationSystem localizationSystem;
        private IPopupController popupController;

        private InformationPopup popup;
        private CancellationTokenSource cts;

        public InformationPopupRouter(
            ITileSystemUIProvidersFactory tileSystemUIProvidersFactory,
            ILocalizationSystem localizationSystem,
            IPopupController popupController
        )
        {
            this.tileSystemUIProvidersFactory = tileSystemUIProvidersFactory;
            this.localizationSystem = localizationSystem;
            this.popupController = popupController;
        }

        public async UniTask ShowPopup(TileConfig tileConfig, CancellationToken cancellationToken)
        {
            if (popup == null)
            {
                popup = popupController.GetPopup<InformationPopup>();
                cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            }

            UpdatePopup(tileConfig);

            if (!popup.Active)
            {
                await popup.Show();
                await WaitForButtonPress(cts.Token);
                await HidePopup();
            }
        }

        public void UpdatePopup(TileConfig tileConfig)
        {
            var viewModule = new InformationViewModule(
                tileSystemUIProvidersFactory,
                localizationSystem,
                new CtsCancelCommand("close", cts),
                tileConfig
            );
            popup.Setup(viewModule);
        }

        public async UniTask HidePopup()
        {
            if (popup == null)
            {
                return;
            }

            await popup.Hide();
            popup = null;
        }

        private async UniTask WaitForButtonPress(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                await UniTask.Yield();
            }
        }
    }
}