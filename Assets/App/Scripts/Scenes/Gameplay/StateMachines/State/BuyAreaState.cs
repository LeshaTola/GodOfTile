using App.Scripts.Scenes.Gameplay.Features.CameraLogic.CameraSwitchers;
using App.Scripts.Scenes.Gameplay.Features.Input;
using App.Scripts.Scenes.Gameplay.Features.Map.Visualizers;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Scenes.Gameplay.StateMachines.State
{
    public class BuyAreaState : Modules.StateMachine.States.General.State
    {
        private IGameInput gameInput;
        private IChunkVisualizer chunkVisualizer;
        private ICameraSwitcher cameraSwitcher;

        public BuyAreaState(string id, IChunkVisualizer chunkVisualizer, IGameInput gameInput,
            ICameraSwitcher cameraSwitcher) : base(id)
        {
            this.chunkVisualizer = chunkVisualizer;
            this.gameInput = gameInput;
            this.cameraSwitcher = cameraSwitcher;
        }

        public override async UniTask Enter()
        {
            await base.Enter();

            gameInput.OnEscape += Back;
            cameraSwitcher.SwitchCamera("buyArea");
            chunkVisualizer.Show();
        }

        public override async UniTask Exit()
        {
            await base.Exit();

            gameInput.OnEscape -= Back;

            cameraSwitcher.SwitchCamera("main");
            
            chunkVisualizer.Hide();
        }

        private async void Back()
        {
            await StateMachine.ChangeState(StatesIds.GAMEPLAY_STATE);
        }
    }
}