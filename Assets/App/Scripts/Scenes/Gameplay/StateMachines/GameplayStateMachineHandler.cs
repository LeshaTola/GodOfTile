using Assets.App.Scripts.Scenes.Gameplay.StateMachines.States;
using Assets.App.Scripts.StateMachines.States;
using Features.StateMachineCore.Factories;
using Modules.StateMachineCore;
using UnityEngine;
using Zenject;

namespace Assets.App.Scripts.Scenes.Gameplay.StateMachines
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
