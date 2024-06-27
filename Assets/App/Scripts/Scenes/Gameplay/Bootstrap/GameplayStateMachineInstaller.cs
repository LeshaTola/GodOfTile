using Assets.App.Scripts.Scenes.Gameplay.StateMachines.States;
using Features.StateMachineCore.Factories;
using Features.StateMachineCore.States;
using Modules.StateMachineCore;
using Zenject;

namespace Assets.App.Scripts.Scenes.Gameplay.Bootstrap
{
	public class GameplayStateMachineInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			BindStatesFactory();
			BindStateMachine();

			BindInitialState();
			BindGameplayState();
		}

		private void BindGameplayState()
		{
			Container.Bind<State>()
				.To<GameplayState>()
				.AsSingle()
				.WithArguments(StatesIds.GAMEPLAY_STATE);
		}

		private void BindInitialState()
		{
			Container.Bind<State>()
				.To<GameplayInitialState>()
				.AsSingle()
				.WithArguments(StatesIds.GAMEPLAY_INITIAL_STATE);
		}

		private void BindStateMachine()
		{
			Container.Bind<StateMachine>().AsSingle();
		}

		private void BindStatesFactory()
		{
			Container.Bind<IStatesFactory>().To<StatesFactory>().AsSingle();
		}
	}
}
