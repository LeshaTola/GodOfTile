using Features.StateMachineCore.States;

namespace Features.StateMachineCore.Factories
{
	public interface IStatesFactory
	{
		State GetState(string id);
	}
}