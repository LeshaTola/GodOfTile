using Features.StateMachineCore.States;
using Modules.StateMachineCore;

namespace Assets.App.Scripts.StateMachines.States
{
	public class GlobalInitialState : State
	{
		public string NextStateId { get; set; }

		private static bool isValid = true;

		public GlobalInitialState(string id) : base(id) { }

		public override void Enter()
		{
			if (!isValid)
			{
				return;
			}

			base.Enter();
			StateMachine.ChangeState(NextStateId);

			isValid = false;
		}
	}
}
