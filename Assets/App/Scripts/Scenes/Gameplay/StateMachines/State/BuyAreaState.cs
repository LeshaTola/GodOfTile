using App.Scripts.Modules.CameraSwitchers;
using App.Scripts.Modules.PopupAndViews.General.Controllers;
using App.Scripts.Modules.StateMachine.Services.UpdateService;
using App.Scripts.Scenes.Gameplay.Features.Commands.GoToStateCommands;
using App.Scripts.Scenes.Gameplay.Features.Commands.Provider;
using App.Scripts.Scenes.Gameplay.Features.Input;
using App.Scripts.Scenes.Gameplay.Features.Map.Visualizers;
using App.Scripts.Scenes.Gameplay.Features.Popups.BuyArea;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Scenes.Gameplay.StateMachines.State
{
    public class BuyAreaState : Modules.StateMachine.States.General.State
    {
        private readonly IGameInput gameInput;
        private readonly ICommandsProvider commandsProvider;
        private readonly IUpdateService updateService;
        private readonly IChunkVisualizer chunkVisualizer;
        private readonly ICameraSwitcher cameraSwitcher;
        private readonly string cameraId;
        private readonly IPopupController popupController;

        private string prevCameraId;

        public BuyAreaState(string id, IChunkVisualizer chunkVisualizer, IGameInput gameInput,
            ICommandsProvider commandsProvider,
            IUpdateService updateService,
            ICameraSwitcher cameraSwitcher, string cameraId, IPopupController popupController) : base(id)
        {
            this.chunkVisualizer = chunkVisualizer;
            this.gameInput = gameInput;
            this.commandsProvider = commandsProvider;
            this.updateService = updateService;
            this.cameraSwitcher = cameraSwitcher;
            this.cameraId = cameraId;
            this.popupController = popupController;
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

        public override async UniTask Update()
        {
            await base.Update();

            updateService.Update();
        }

        public override async UniTask Exit()
        {
            await base.Exit();

            gameInput.OnEscape -= Back;
            gameInput.OnM -= Back;

            await popupController.HidePopup<BuyAreaPopup>();
            cameraSwitcher.SwitchCamera(prevCameraId);

            chunkVisualizer.Hide();
        }

        private void Back()
        {
            commandsProvider.GetCommand<GoToGamePlayStateCommand>().Execute();
        }
    }
}