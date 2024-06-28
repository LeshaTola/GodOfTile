using System.Collections.Generic;
using System.Linq;
using Features.StateMachineCore.States;
using Modules.StateMachineCore;
using UnityEngine;
using Zenject;

namespace Features.StateMachineCore.Factories
{
    public class StatesFactory : IStatesFactory
    {
        private DiContainer diContainer;

        public StatesFactory(DiContainer diContainer)
        {
            this.diContainer = diContainer;
        }

        public State GetState(string id)
        {
            List<State> states = diContainer.ResolveAll<State>();
            State state = states.FirstOrDefault(x => x.Id.Equals(id));
            if (state != null)
            {
                state.Initialize(diContainer.Resolve<StateMachine>());
            }
            else
            {
                Debug.LogWarning($"Can't find {id} state");
            }

            return state;
        }
    }
}
