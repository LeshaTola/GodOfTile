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
            BindBuildState();
        }

        private void BindBuildState()
        {
            BindState<BuildState>(StatesIds.BUILDING_STATE);
        }

        private void BindGameplayState()
        {
            BindState<GameplayState>(StatesIds.GAMEPLAY_STATE);
        }

        private void BindInitialState()
        {
            BindState<GameplayInitialState>(StatesIds.GAMEPLAY_INITIAL_STATE);
        }

        private void BindStateMachine()
        {
            Container.Bind<StateMachine>().AsSingle();
        }

        private void BindStatesFactory()
        {
            Container.Bind<IStatesFactory>().To<StatesFactory>().AsSingle();
        }

        private void BindState<T>(string stateId)
            where T : State
        {
            Container.Bind<State>().To<T>().AsSingle().WithArguments(stateId);
        }
    }
}
