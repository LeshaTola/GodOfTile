using App.Scripts.Features.SceneTransitions;
using App.Scripts.Modules.CameraSwitchers;
using App.Scripts.Modules.Saves;
using App.Scripts.Modules.StateMachine.Services.InitializeService;
using App.Scripts.Modules.Tasks.Providers;
using App.Scripts.Scenes.Gameplay.Features.Saves;
using App.Scripts.Scenes.Gameplay.StateMachines.Ids;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Scenes.Gameplay.StateMachines.State
{
    public class GameplayInitialState : Modules.StateMachine.States.General.State
    {
        private IInitializeService initializeService;
        private ICameraSwitcher cameraSwitcher;
        private readonly ISceneTransition sceneTransition;
        private readonly GameplaySavesController savesController;
        private readonly TasksProvider tasksProvider;
        private string cameraId;

        public GameplayInitialState(string id,
            IInitializeService initializeService,
            ICameraSwitcher cameraSwitcher,
            ISceneTransition sceneTransition,
            GameplaySavesController savesController,
            TasksProvider tasksProvider,
            string cameraId)
            : base(id)
        {
            this.initializeService = initializeService;
            this.cameraSwitcher = cameraSwitcher;
            this.sceneTransition = sceneTransition;
            this.savesController = savesController;
            this.tasksProvider = tasksProvider;
            this.cameraId = cameraId;
        }

        public override async UniTask Enter()
        {
            await base.Enter();
            initializeService.Initialize();
            cameraSwitcher.SwitchCamera(cameraId);
            savesController.Load();
            tasksProvider.FillTasks();
            await StateMachine.ChangeState(StatesIds.GAMEPLAY_STATE);
        }

        public override async UniTask Exit()
        {
            await base.Exit();
            await sceneTransition.PlayOffAsync();
        }
    }
}