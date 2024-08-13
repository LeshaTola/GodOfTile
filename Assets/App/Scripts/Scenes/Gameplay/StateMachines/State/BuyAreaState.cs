using App.Scripts.Scenes.Gameplay.Features.Input;
using App.Scripts.Scenes.Gameplay.Features.Map.Visualizers;
using Cysharp.Threading.Tasks;
using UnityEditor.VersionControl;

namespace App.Scripts.Scenes.Gameplay.StateMachines.State
{
    public class BuyAreaState : Modules.StateMachine.States.General.State
    {
        private IGameInput gameInput;
        private IChunkVisualizer chunkVisualizer;

        public BuyAreaState(string id, IChunkVisualizer chunkVisualizer, IGameInput gameInput) : base(id)
        {
            this.chunkVisualizer = chunkVisualizer;
            this.gameInput = gameInput;
        }

        public override async UniTask Enter()
        {
            await base.Enter();

            gameInput.OnEscape += Back;

            chunkVisualizer.Show();
        }

        public override async UniTask Exit()
        {
            await base.Exit();

            gameInput.OnEscape -= Back;
            
            chunkVisualizer.Hide();
        }

        private async void Back()
        {
            await StateMachine.ChangeState(StatesIds.GAMEPLAY_STATE);
        }
    }
}