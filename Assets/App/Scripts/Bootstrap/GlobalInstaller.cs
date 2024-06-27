using Assets.App.Scripts.StateMachines.States;
using Features.StateMachineCore.States;
using Zenject;

namespace Assets.App.Scripts.Bootstrap
{
	public class GlobalInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			BindGlobalInitialState();
		}

		private void BindGlobalInitialState()
		{
			Container.Bind<State>()
				.To<GlobalInitialState>()
				.AsSingle()
				.WithArguments(GlobalStatesIds.GLOBAL_INITIAL_STATE);
		}
	}
}
