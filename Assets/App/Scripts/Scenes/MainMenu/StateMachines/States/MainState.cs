using App.Scripts.Modules.StateMachine.Services.UpdateService;
using App.Scripts.Scenes.Gameplay.Features.Commands.Provider;
using App.Scripts.Scenes.Gameplay.Features.Input;
using App.Scripts.Scenes.Gameplay.Features.Shop.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Providers;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Services.ChunkFilling;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Services.TilesCreation;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Providers.Selection;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Scenes.MainMenu.StateMachines.States
{
    public class MainState : Modules.StateMachine.States.General.State
    {
        private readonly IUpdateService updateService;
        private readonly IGameInput gameInput;
        private readonly ITileSelectionProvider tileSelectionProvider;
        private readonly ChunkFillingService chunkFillingService;
        private readonly ITilesCreationService tilesCreationService;
        private readonly ChunkFilling chunkFilling;

        public MainState(
            string id,
            IUpdateService updateService,
            IGameInput gameInput,
            ITileSelectionProvider tileSelectionProvider,
            ChunkFilling chunkFilling, 
            ChunkFillingService chunkFillingService)
            : base(id)
        {
            this.updateService = updateService;
            this.gameInput = gameInput;
            this.tileSelectionProvider = tileSelectionProvider;
            this.chunkFilling = chunkFilling;
            this.chunkFillingService = chunkFillingService;
        }

        public override async UniTask Enter()
        {
            await chunkFillingService.GenerateChankAsync(chunkFilling);
            await base.Enter();
        }

        public override async UniTask Update()
        {
            await base.Update();
            updateService.Update();

            if (gameInput.IsMouseClicked())
            {
                var tile = tileSelectionProvider.GetTileAtMousePosition();
                
                
                if (tile == null)
                {
                    return;
                }

                tileSelectionProvider.SelectTile(tile);
            }
        }

        public override async UniTask Exit()
        {
            await base.Exit();
            tileSelectionProvider.Cleanup();
        }
    }
}