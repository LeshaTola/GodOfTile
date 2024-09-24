using App.Scripts.Features.StateMachines.States;
using App.Scripts.Modules.StateMachine;
using App.Scripts.Modules.StateMachine.Factories.States;
using App.Scripts.Scenes.Gameplay.StateMachines.Ids;
using UnityEngine;
using Zenject;

namespace App.Scripts.Scenes.Gameplay.StateMachines
{
    public class GameplayStateMachineHandler : MonoBehaviour
    {
        private StateMachine stateMachine;
        private IStatesFactory statesFactory;

        [Inject]
        public void Construct(StateMachine stateMachine, IStatesFactory statesFactory)
        {
            this.stateMachine = stateMachine;
            this.statesFactory = statesFactory;
        }

        private async void Start()
        {
            var startState = (GlobalInitialState)
                statesFactory.GetState(GlobalStatesIds.GLOBAL_INITIAL_STATE);
            startState.NextStateId = StatesIds.GAMEPLAY_INITIAL_STATE;
            await stateMachine.ChangeState(startState);
        }

        private void Update()
        {
            stateMachine.Update();
        }
    }
}