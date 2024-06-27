using Features.StateMachineCore.States.General;
using Zenject;

namespace Features.StateMachineCore.Factories
{
	public class StateStepsFactory : IStateStepsFactory
	{
		DiContainer diContainer;

		public StateStepsFactory(DiContainer diContainer)
		{
			this.diContainer = diContainer;
		}

		public T GetStateStep<T>() where T : StateStep
		{
			return diContainer.Resolve<T>();
		}
	}
}
