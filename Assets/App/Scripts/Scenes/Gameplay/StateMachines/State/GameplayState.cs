using System.Collections.Generic;
using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Providers;
using Cysharp.Threading.Tasks;
using Features.StateMachineCore;
using Features.StateMachineCore.States;

namespace Assets.App.Scripts.Scenes.Gameplay.StateMachines.States
{
    public class GameplayState : State
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
