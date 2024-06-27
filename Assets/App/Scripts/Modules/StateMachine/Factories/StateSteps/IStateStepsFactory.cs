using Features.StateMachineCore.States.General;

namespace Features.StateMachineCore.Factories
{
	public interface IStateStepsFactory
	{
		T GetStateStep<T>() where T : StateStep;
	}
}