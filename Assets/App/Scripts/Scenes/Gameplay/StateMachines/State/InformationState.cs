using System.Collections.Generic;
using App.Scripts.Modules.StateMachine.Services.UpdateService;
using App.Scripts.Scenes.Gameplay.Features.Input;
using App.Scripts.Scenes.Gameplay.Features.Popups.Information.Routers;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Providers.Selection;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.Views;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Scenes.Gameplay.StateMachines.State
{
    public class InformationState : Modules.StateMachine.States.General.State
    {
        private List<IUpdatable> updatables;
        private IGameInput gameInput;
        private IInformationPopupRouter informationPopupRouter;
        private ITileSelectionProvider tileSelectionProvider;
        private IEffectorVisualProvider effectorVisualProvider;

        public InformationState(
            string id,
            List<IUpdatable> updatables,
            IInformationPopupRouter informationPopupRouter,
            ITileSelectionProvider tileSelectionProvider,
            IEffectorVisualProvider effectorVisualProvider,
            IGameInput gameInput
        )
            : base(id)
        {
            this.updatables = updatables;
            this.informationPopupRouter = informationPopupRouter;
            this.tileSelectionProvider = tileSelectionProvider;
            this.gameInput = gameInput;
            this.effectorVisualProvider = effectorVisualProvider;
        }

        public override async UniTask Enter()
        {
            await base.Enter();
            await ShowTileInformation();
        }

        public override async UniTask Update()
        {
            await base.Update();

            foreach (var updatable in updatables)
            {
                updatable.Update();
            }

            if (gameInput.IsMouseClicked())
            {
                await ShowTileInformation();
            }
        }

        public override async UniTask Exit()
        {
            await base.Exit();
            await informationPopupRouter.HideInformationPopup();
            effectorVisualProvider.Cleanup();
        }

        private async UniTask ShowTileInformation()
        {
            var tile = tileSelectionProvider.GetTileAtMousePosition();

            if (tile == null)
            {
                return;
            }

            effectorVisualProvider.Cleanup();
            effectorVisualProvider.Setup(tile);
            
            await informationPopupRouter.ShowInformationPopup(tile.Config);
        }
    }
}