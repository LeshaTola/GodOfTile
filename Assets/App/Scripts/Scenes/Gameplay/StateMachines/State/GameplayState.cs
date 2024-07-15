using System.Collections.Generic;
using App.Scripts.Modules.StateMachine.Services.UpdateService;
using App.Scripts.Scenes.Gameplay.Features.Input;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Providers.Selection;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Scenes.Gameplay.StateMachines.State
{
    public class GameplayState : Modules.StateMachine.States.General.State
    {
        private List<IUpdatable> updatables;
        private IGameInput gameInput;
        private ITileSelectionProvider tileSelectionProvider;

        public GameplayState(
            string id,
            List<IUpdatable> updatables,
            IGameInput gameInput,
            ITileSelectionProvider tileSelectionProvider
        )
            : base(id)
        {
            this.updatables = updatables;
            this.gameInput = gameInput;
            this.tileSelectionProvider = tileSelectionProvider;
        }

        public override async UniTask Enter()
        {
            await base.Enter();

            gameInput.OnBuild += OnBuild;
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
                var tile = tileSelectionProvider.GetTileAtMousePosition();

                if (tile == null)
                {
                    return;
                }

                await StateMachine.ChangeState(StatesIds.INFORMATION_STATE);
            }
        }

        public override async UniTask Exit()
        {
            await base.Exit();

            gameInput.OnBuild -= OnBuild;
        }

        private async void OnBuild()
        {
            await StateMachine.ChangeState(StatesIds.BUILDING_STATE);
        }
    }
}