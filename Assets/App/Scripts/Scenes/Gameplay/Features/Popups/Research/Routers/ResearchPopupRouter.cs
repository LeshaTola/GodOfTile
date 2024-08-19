using App.Scripts.Modules.Localization;
using App.Scripts.Modules.PopupLogic.General.Controllers;
using App.Scripts.Scenes.Gameplay.Features.Popups.Research.ViewModels;
using App.Scripts.Scenes.Gameplay.Features.Researches.Services;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Scenes.Gameplay.Features.Popups.Research.Routers
{
    public class ResearchPopupRouter
    {
        private IPopupController popupController;
        private ILocalizationSystem localizationSystem;
        private IResearchService researchService;

        public ResearchPopupRouter(IPopupController popupController, ILocalizationSystem localizationSystem,
            IResearchService researchService)
        {
            this.popupController = popupController;
            this.localizationSystem = localizationSystem;
            this.researchService = researchService;
        }

        private ResearchPopup popup;

        public async UniTask ShowPopup()
        {
            if (popup == null)
            {
                popup = popupController.GetPopup<ResearchPopup>();
            }

            var viewModule = new ResearchPopupViewModule(
                localizationSystem,
                researchService
            );
            popup.Setup(viewModule);

            await popup.Show();
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
    }
}