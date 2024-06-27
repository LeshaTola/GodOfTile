using Features.StateMachineCore.Factories;
using Features.StateMachineCore.States;
using Features.StateMachineCore.States.General;
using System.Collections.Generic;

namespace Modules.StateMachineCore
{
	public class StateMachine
	{
		private State currentState;
		private Dictionary<string, State> states = new();
		private IStatesFactory statesFactory;

		public StateMachine(IStatesFactory statesFactory)
		{
			this.statesFactory = statesFactory;
		}

		public State CurrentState { get => currentState; }

		public void AddState(State state)
		{
			states.Add(state.Id, state);
		}

		public void ChangeState(State state)
		{
			ChangeState(state.Id);
		}

		public void ChangeState(string id)
		{
			if (currentState != null && currentState.Id.Equals(id))
			{
				return;
			}

			if (!states.ContainsKey(id))
			{
				var state = statesFactory.GetState(id);
				if (state == null)
				{
					return;
				}

				states.Add(id, state);
			}

			currentState?.Exit();
			currentState = states[id];
			currentState.Enter();
		}

		public void Update()
		{
			currentState?.Update();
		}

		public void AddStep(string stateId, IStateStep stateStep)
		{
			if (states.ContainsKey(stateId))
			{
				states[stateId]?.AddStep(stateStep);
			}
		}
	}
}