using App.Scripts.Modules.CameraSwitchers;
using App.Scripts.Scenes.Gameplay.Features.Commands.GoToStateCommands;
using App.Scripts.Scenes.Gameplay.Features.Commands.Provider;
using App.Scripts.Scenes.Gameplay.Features.Input;
using App.Scripts.Scenes.Gameplay.Features.Map.Visualizers;
using App.Scripts.Scenes.Gameplay.StateMachines.Ids;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Scenes.Gameplay.StateMachines.State
{
    public class BuyAreaState : Modules.StateMachine.States.General.State
    {
        private IGameInput gameInput;
        private ICommandsProvider commandsProvider;
        private IChunkVisualizer chunkVisualizer;
        private ICameraSwitcher cameraSwitcher;
        private string cameraId;

        private string prevCameraId;

        public BuyAreaState(string id, IChunkVisualizer chunkVisualizer, IGameInput gameInput,
            ICommandsProvider commandsProvider,
            ICameraSwitcher cameraSwitcher, string cameraId) : base(id)
        {
            this.chunkVisualizer = chunkVisualizer;
            this.gameInput = gameInput;
            this.commandsProvider = commandsProvider;
            this.cameraSwitcher = cameraSwitcher;
            this.cameraId = cameraId;
        }

        public override async UniTask Enter()
        {
            await base.Enter();

            gameInput.OnEscape += Back;
            gameInput.OnM += Back;

            prevCameraId = cameraSwitcher.CurrentCameraId;
            cameraSwitcher.SwitchCamera(cameraId);

            chunkVisualizer.Show();
        }

        public override async UniTask Exit()
        {
            await base.Exit();

            gameInput.OnEscape -= Back;
            gameInput.OnM -= Back;

            cameraSwitcher.SwitchCamera(prevCameraId);

            chunkVisualizer.Hide();
        }

        private void Back()
        {
            commandsProvider.GetCommand<GoToGamePlayStateCommand>().Execute();
        }
    }
}