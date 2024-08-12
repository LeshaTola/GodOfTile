using System.Collections.Generic;
using App.Scripts.Modules.Localization;
using App.Scripts.Modules.PopupLogic.General.Controllers;
using App.Scripts.Scenes.Gameplay.Features.Commands.BuyAreaCommand;
using App.Scripts.Scenes.Gameplay.Features.Commands.GoToGamePlayStateCommands;
using App.Scripts.Scenes.Gameplay.Features.Inventory.DTO;
using App.Scripts.Scenes.Gameplay.Features.Map.Providers.Cost;
using App.Scripts.Scenes.Gameplay.Features.Popups.BuyAreaPopup.ViewModules;
using App.Scripts.Scenes.Gameplay.Features.Popups.ShopPopup.InformationWidget.ViewModels;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Popups.BuyAreaPopup.Routers
{
    public class BuyAreaPopupRouter : IBuyAreaPopupRouter
    {
        private ILocalizationSystem localizationSystem;
        private IPopupController popupController;
        private IInformationWidgetViewModule informationWidgetViewModule;
        private IChunkCostProvider chunkCostProvider;
        private GoToGamePlayStateCommand closeCommand;
        private BuyAreaCommand buyCommand;

        private BuyAreaPopup popup;

        public BuyAreaPopupRouter(ILocalizationSystem localizationSystem, IPopupController popupController,
            IInformationWidgetViewModule informationWidgetViewModule, IChunkCostProvider chunkCostProvider, GoToGamePlayStateCommand closeCommand,
            BuyAreaCommand buyCommand)
        {
            this.localizationSystem = localizationSystem;
            this.popupController = popupController;
            this.informationWidgetViewModule = informationWidgetViewModule;
            this.chunkCostProvider = chunkCostProvider;
            this.closeCommand = closeCommand;
            this.buyCommand = buyCommand;
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

        public async UniTask Show(Vector2Int chinkId)
        {
            if (popup == null)
            {
                popup = popupController.GetPopup<BuyAreaPopup>();
            }

            buyCommand.ChunkId = chinkId;
            var resources = chunkCostProvider.GetCost(chinkId);
            
            var viewModule = new BuyAreaPopupViewModule(resources, localizationSystem, informationWidgetViewModule,
                buyCommand, closeCommand);
            popup.Setup(viewModule);

            await popup.Show();
        }
    }
}