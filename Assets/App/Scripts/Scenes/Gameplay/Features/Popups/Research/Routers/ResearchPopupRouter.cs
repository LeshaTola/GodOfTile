using App.Scripts.Modules.Factories;
using App.Scripts.Modules.Localization;
using App.Scripts.Modules.PopupLogic.General.Controllers;
using App.Scripts.Scenes.Gameplay.Features.Inventory.Systems;
using App.Scripts.Scenes.Gameplay.Features.Popups.InformationWidget.ViewModels;
using App.Scripts.Scenes.Gameplay.Features.Popups.Research.Elements.Level;
using App.Scripts.Scenes.Gameplay.Features.Popups.Research.Elements.Research;
using App.Scripts.Scenes.Gameplay.Features.Popups.Research.ViewModels;
using App.Scripts.Scenes.Gameplay.Features.Researches.Services;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Scenes.Gameplay.Features.Popups.Research.Routers
{
    public class ResearchPopupRouter
    {
        private readonly IPopupController popupController;
        private readonly ILocalizationSystem localizationSystem;
        private readonly IResearchService researchService;
        private readonly IInventorySystem inventorySystem;
        private readonly IFactory<LevelElement> levelsFactory;
        private readonly IFactory<ResearchElement> researchesFactory;
        private readonly IInformationWidgetViewModule informationWidgetViewModule;

        public ResearchPopupRouter(IPopupController popupController, ILocalizationSystem localizationSystem,
            IResearchService researchService, IFactory<LevelElement> levelsFactory,
            IFactory<ResearchElement> researchesFactory, IInformationWidgetViewModule informationWidgetViewModule,
            IInventorySystem inventorySystem)
        {
            this.popupController = popupController;
            this.localizationSystem = localizationSystem;
            this.researchService = researchService;
            this.levelsFactory = levelsFactory;
            this.researchesFactory = researchesFactory;
            this.informationWidgetViewModule = informationWidgetViewModule;
            this.inventorySystem = inventorySystem;
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
                researchService,
                levelsFactory,
                researchesFactory,
                informationWidgetViewModule,
                inventorySystem
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