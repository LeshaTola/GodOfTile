using App.Scripts.Modules.CameraSwitchers;
using App.Scripts.Modules.Localization;
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

        public InitialState(string id, IInitializeService initializeService, ICameraSwitcher cameraSwitcher,
            string cameraId, TileInformationPresenter tileInformationPresenter)
            : base(id)
        {
            this.initializeService = initializeService;
            this.cameraSwitcher = cameraSwitcher;
            this.cameraId = cameraId;
            this.tileInformationPresenter = tileInformationPresenter;
        }

        public override async UniTask Enter()
        {
            await base.Enter();
            initializeService.Initialize();
            cameraSwitcher.SwitchCamera(cameraId);
            await StateMachine.ChangeState(StatesIds.MAIN_STATE);
            tileInformationPresenter.Initialize();
        }
    }
}