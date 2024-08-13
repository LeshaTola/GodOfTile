using App.Scripts.Modules.StateMachine;
using App.Scripts.Modules.StateMachine.Factories.States;
using App.Scripts.Modules.StateMachine.States.General;
using App.Scripts.Scenes.Gameplay.StateMachines.State;
using Zenject;

namespace App.Scripts.Scenes.Gameplay.Bootstrap
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
            BindInformationState();
            BindState<BuyAreaState>(StatesIds.BUY_AREA_STATE);
        }

        private void BindInformationState()
        {
            BindState<InformationState>(StatesIds.INFORMATION_STATE);
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