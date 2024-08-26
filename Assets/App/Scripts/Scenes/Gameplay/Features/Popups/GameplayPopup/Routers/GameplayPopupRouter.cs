using App.Scripts.Modules.Localization;
using App.Scripts.Modules.PopupLogic.General.Controllers;
using App.Scripts.Scenes.Gameplay.Features.Commands.GoToStateCommands;
using App.Scripts.Scenes.Gameplay.Features.Popups.GameplayPopup.ViewModels;
using App.Scripts.Scenes.Gameplay.Features.Researches.Services;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Scenes.Gameplay.Features.Popups.GameplayPopup.Routers
{
    public class GameplayPopupRouter : IGameplayPopupRouter
    {
        private ILocalizationSystem localizationSystem;
        private GoToBuildingStateCommand buildingStateCommand;
        private GoToBuyAreaStateCommand buyAreaStateCommand;
        private IPopupController popupController;
        private readonly IResearchService researchService;

        private GameplayPopup popup;

        public GameplayPopupRouter(ILocalizationSystem localizationSystem,
            GoToBuildingStateCommand buildingStateCommand, GoToBuyAreaStateCommand buyAreaStateCommand,
            IPopupController popupController, IResearchService researchService)
        {
            this.localizationSystem = localizationSystem;
            this.buildingStateCommand = buildingStateCommand;
            this.buyAreaStateCommand = buyAreaStateCommand;
            this.popupController = popupController;
            this.researchService = researchService;
        }

        public async UniTask Show()
        {
            popup = popupController.GetPopup<GameplayPopup>();

            var viewModel
                = new GameplayPopupViewModel(
                    localizationSystem,
                    buildingStateCommand, 
                    buyAreaStateCommand,
                    researchService
                    );
            popup.Setup(viewModel);
            await popup.Show();
        }

        public async UniTask Hide()
        {
            if (popup == null)
            {
                return;
            }

            await popup.Hide();
            popup = null;
        }
    }
}