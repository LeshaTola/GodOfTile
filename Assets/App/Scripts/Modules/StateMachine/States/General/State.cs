using Features.StateMachineCore.States.General;
using Modules.StateMachineCore;
using System.Collections.Generic;

namespace Features.StateMachineCore.States
{
	public abstract class State
	{
		protected StateMachine StateMachine;
		protected List<IStateStep> StateSteps;

		protected State(string id)
		{
			Id = id;
		}

		public void Initialize(StateMachine stateMachine)
		{
			StateMachine = stateMachine;
			StateSteps = new();
		}

		public string Id { get; private set; }

		public virtual void Enter()
		{
			foreach (var step in StateSteps)
			{
				step.Enter();
			}
		}

		public virtual void Exit()
		{
			foreach (var step in StateSteps)
			{
				step.Exit();
			}

		}

		public virtual void Update()
		{

			foreach (var step in StateSteps)
			{
				step.Update();
			}
		}

		public void AddStep(IStateStep step)
		{
			StateSteps.Add(step);
			step.Init(this, StateMachine);
		}
		public void AddSteps(IEnumerable<IStateStep> steps)
		{
			foreach (var step in steps)
			{
				AddStep(step);
			}
		}

		public void RemoveStep(IStateStep step)
		{
			StateSteps.Remove(step);
		}
	}
}