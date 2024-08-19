using App.Scripts.Modules.Localization;
using App.Scripts.Modules.PopupLogic.General.Controllers;
using App.Scripts.Scenes.Gameplay.Features.Commands;
using App.Scripts.Scenes.Gameplay.Features.Commands.BuyAreaCommand;
using App.Scripts.Scenes.Gameplay.Features.Map.Providers.Chunk.Cost;
using App.Scripts.Scenes.Gameplay.Features.Popups.BuyArea.ViewModules;
using App.Scripts.Scenes.Gameplay.Features.Popups.InformationWidget.ViewModels;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Popups.BuyArea.Routers
{
    public class BuyAreaPopupRouter : IBuyAreaPopupRouter
    {
        private ILocalizationSystem localizationSystem;
        private IPopupController popupController;
        private IInformationWidgetViewModule informationWidgetViewModule;
        private IChunkCostProvider chunkCostProvider;
        private ClosePopupCommand closeCommand;
        private BuyAreaCommand buyCommand;

        private BuyAreaPopup popup;

        public BuyAreaPopupRouter(ILocalizationSystem localizationSystem, IPopupController popupController,
            IInformationWidgetViewModule informationWidgetViewModule, IChunkCostProvider chunkCostProvider,
            ClosePopupCommand closeCommand, BuyAreaCommand buyCommand)
        {
            this.localizationSystem = localizationSystem;
            this.popupController = popupController;
            this.informationWidgetViewModule = informationWidgetViewModule;
            this.chunkCostProvider = chunkCostProvider;
            this.closeCommand = closeCommand;
            this.buyCommand = buyCommand;
        }
        
        public async UniTask Show(Vector2Int chinkId)
        {
            popup = popupController.GetPopup<BuyAreaPopup>();
            
            buyCommand.ChunkId = chinkId;
            var resources = chunkCostProvider.GetCost(chinkId);

            var viewModule = new BuyAreaPopupViewModule(resources, localizationSystem, informationWidgetViewModule,
                buyCommand, closeCommand);
            popup.Setup(viewModule);

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