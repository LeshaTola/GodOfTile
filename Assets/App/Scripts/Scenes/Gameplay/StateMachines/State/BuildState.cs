using System.Collections.Generic;
using Assets.App.Scripts.Scenes.Gameplay.Features.Creation.Services;
using Features.StateMachineCore;
using Features.StateMachineCore.States;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.App.Scripts.Scenes.Gameplay.StateMachines.States
{
    public class BuildState : State
    {
        private List<IUpdatable> updatables;
        private IGameInput gameInput;
        private ICreationService creationService;

        public BuildState(
            List<IUpdatable> updatables,
            string id,
            Camera mainCamera,
            IGameInput gameInput,
            ICreationService creationService
        )
            : base(id)
        {
            this.updatables = updatables;
            this.gameInput = gameInput;
            this.creationService = creationService;
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("BuildState");

            gameInput.OnBuild += OnBuild;
            creationService.StartPlacingTile();
        }

        public override void Update()
        {
            base.Update();

            foreach (var updatable in updatables)
            {
                updatable.Update();
            }

            creationService.MoveActiveTile(gameInput.GetGroundMousePosition());

            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                creationService.PlaceActiveTile();
                creationService.StartPlacingTile();
            }
        }

        public override void Exit()
        {
            base.Exit();

            creationService.StopPlacingTile();
            gameInput.OnBuild -= OnBuild;
        }

        private void OnBuild()
        {
            StateMachine.ChangeState(StatesIds.GAMEPLAY_STATE);
        }
    }
}
