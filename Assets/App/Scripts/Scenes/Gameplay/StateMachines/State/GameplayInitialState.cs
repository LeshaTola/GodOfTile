﻿using App.Scripts.Modules.CameraSwitchers;
using App.Scripts.Modules.StateMachine.Services.InitializeService;
using App.Scripts.Scenes.Gameplay.StateMachines.Ids;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Scenes.Gameplay.StateMachines.State
{
    public class GameplayInitialState : Modules.StateMachine.States.General.State
    {
        private IInitializeService initializeService;
        private ICameraSwitcher cameraSwitcher;
        private string cameraId;

        public GameplayInitialState(string id, IInitializeService initializeService, ICameraSwitcher cameraSwitcher,
            string cameraId)
            : base(id)
        {
            this.initializeService = initializeService;
            this.cameraSwitcher = cameraSwitcher;
            this.cameraId = cameraId;
        }

        public override async UniTask Enter()
        {
            await base.Enter();
            initializeService.Initialize();
            cameraSwitcher.SwitchCamera(cameraId);
            await StateMachine.ChangeState(StatesIds.GAMEPLAY_STATE);
        }
    }
}