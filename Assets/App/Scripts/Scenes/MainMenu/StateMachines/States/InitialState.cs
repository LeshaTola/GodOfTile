using System.Collections.Generic;
using App.Scripts.Features.SceneTransitions;
using App.Scripts.Modules.CameraSwitchers;
using App.Scripts.Modules.Localization;
using App.Scripts.Modules.Saves;
using App.Scripts.Modules.StateMachine.Services.InitializeService;
using App.Scripts.Scenes.Gameplay.Features.Screens.Gameplay.TileInformation;
using App.Scripts.Scenes.Gameplay.Features.Screens.Gameplay.TileInformation.Presenters;
using App.Scripts.Scenes.MainMenu.StateMachines.Ids;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Scenes.MainMenu.StateMachines.States
{
    public class InitialState : Modules.StateMachine.States.General.State
    {
        private IInitializeService initializeService;
        private ICameraSwitcher cameraSwitcher;
        private string cameraId;
        private readonly TileInformationPresenter tileInformationPresenter;
        private readonly List<ISavable> savables;
        private readonly ISceneTransition sceneTransition;

        public InitialState(
            TileInformationPresenter tileInformationPresenter,
            IInitializeService initializeService,
            ISceneTransition sceneTransition,
            ICameraSwitcher cameraSwitcher,
            List<ISavable> savables, 
            string cameraId,
            string id)
            : base(id)
        {
            this.initializeService = initializeService;
            this.cameraSwitcher = cameraSwitcher;
            this.cameraId = cameraId;
            this.tileInformationPresenter = tileInformationPresenter;
            this.savables = savables;
            this.sceneTransition = sceneTransition;
        }

        public override async UniTask Enter()
        {
            await base.Enter();
            initializeService.Initialize();
            cameraSwitcher.SwitchCamera(cameraId);

            foreach (var savable in savables)
            {
                savable.LoadState();
            }
            
            await StateMachine.ChangeState(StatesIds.MAIN_STATE);
            tileInformationPresenter.Initialize();
        }

        public override async UniTask Exit()
        {
            await base.Exit();
            await sceneTransition.PlayOffAsync();
        }
    }
}