﻿using System.Collections.Generic;
using Features.StateMachineCore;
using Features.StateMachineCore.States;
using UnityEngine;

namespace Assets.App.Scripts.Scenes.Gameplay.StateMachines.States
{
    public class GameplayState : State
    {
        private List<IUpdatable> updatables;
        private IGameInput gameInput;

        public GameplayState(List<IUpdatable> updatables, IGameInput gameInput, string id)
            : base(id)
        {
            this.updatables = updatables;
            this.gameInput = gameInput;
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("gameplayState");

            gameInput.OnBuild += OnBuild;
        }

        public override void Update()
        {
            base.Update();

            foreach (var updatable in updatables)
            {
                updatable.Update();
            }
        }

        public override void Exit()
        {
            base.Exit();

            gameInput.OnBuild -= OnBuild;
        }

        private void OnBuild()
        {
            StateMachine.ChangeState(StatesIds.BUILDING_STATE);
        }
    }
}
