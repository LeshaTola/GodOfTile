using App.Scripts.Modules.StateMachine.Services.UpdateService;
using App.Scripts.Scenes.Gameplay.Features.Commands.Provider;
using App.Scripts.Scenes.Gameplay.Features.Input;
using App.Scripts.Scenes.Gameplay.Features.Shop.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Providers;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Services.TilesCreation;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Providers.Selection;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Scenes.MainMenu.StateMachines.States
{
    public class MainState : Modules.StateMachine.States.General.State
    {
        private IUpdateService updateService;
        private IGameInput gameInput;
        private ICommandsProvider commandsProvider;
        private ITileSelectionProvider tileSelectionProvider;
        private readonly ITilesCreationService tilesCreationService;
        private readonly IActiveTileProvider activeTileProvider;
        private readonly CollectionConfig collectionConfig;


        public MainState(
            string id,
            IUpdateService updateService,
            IGameInput gameInput,
            ICommandsProvider commandsProvider,
            ITileSelectionProvider tileSelectionProvider,
            ITilesCreationService tilesCreationService,
            IActiveTileProvider activeTileProvider,
            CollectionConfig collectionConfig
        )
            : base(id)
        {
            this.updateService = updateService;
            this.gameInput = gameInput;
            this.commandsProvider = commandsProvider;
            this.tileSelectionProvider = tileSelectionProvider;
            this.tilesCreationService = tilesCreationService;
            this.activeTileProvider = activeTileProvider;
            this.collectionConfig = collectionConfig;
        }

        public override async UniTask Enter()
        {
            await base.Enter();
            tilesCreationService.StartPlacingTile();
            
            activeTileProvider.ActiveTileConfig = collectionConfig.StartTiles[0];
            tilesCreationService.MoveActiveTile(new(4,3));
            tilesCreationService.PlaceActiveTile();
            
            activeTileProvider.ActiveTileConfig = collectionConfig.StartTiles[1];
            tilesCreationService.MoveActiveTile(new(4,4));
            tilesCreationService.PlaceActiveTile();
            
            activeTileProvider.ActiveTileConfig = collectionConfig.StartTiles[2];
            tilesCreationService.MoveActiveTile(new(4,5));
            tilesCreationService.PlaceActiveTile();
            
             tilesCreationService.StopPlacingTile();
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