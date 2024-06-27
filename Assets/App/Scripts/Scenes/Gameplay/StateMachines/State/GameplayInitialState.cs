using Features.StateMachineCore.States;

namespace Assets.App.Scripts.Scenes.Gameplay.StateMachines.States
{
	public class GameplayInitialState : State
	{
		public GameplayInitialState(string id) : base(id) { }

		public override void Enter()
		{
			base.Enter();
			StateMachine.ChangeState(StatesIds.GAMEPLAY_STATE);
		}
	}
}
