using System.Collections.Generic;
using App.Scripts.Features.StateMachines.States;
using App.Scripts.Modules.CameraSwitchers;
using App.Scripts.Modules.StateMachine;
using App.Scripts.Modules.StateMachine.Factories.States;
using App.Scripts.Modules.StateMachine.States.General;
using App.Scripts.Scenes.Gameplay.StateMachines.Ids;
using App.Scripts.Scenes.Gameplay.StateMachines.State;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace App.Scripts.Scenes.Gameplay.Bootstrap
{
    public class GameplayStateMachineInstaller : MonoInstaller
    {
        [SerializeField] private CamerasDatabase camerasDatabase;
        [SerializeField] private string sceneName = "MainMenu";

        [ValueDropdown(nameof(GetCamerasIds))]
        [SerializeField] private string mainCameraId;

        [ValueDropdown(nameof(GetCamerasIds))]
        [SerializeField] private string buyAreaCameraId;

        public override void InstallBindings()
        {
            BindStatesFactory();
            BindStateMachine();

            BindInitialState();
            BindGameplayState();
            BindBuildState();
            BindBuyAreaState();
            BindState<ResearchState>(StatesIds.RESEARCH_STATE);
            BindState<PauseState>(StatesIds.PAUSE_STATE);
            Container.Bind<State>().To<LoadSceneState>().AsSingle()
                .WithArguments(StatesIds.LOAD_SCENE_STATE, sceneName);
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
            Container.Bind<State>().To<GameplayInitialState>().AsSingle()
                .WithArguments(StatesIds.GAMEPLAY_INITIAL_STATE, mainCameraId);
        }

        private void BindStateMachine()
        {
            Container.Bind<StateMachine>().AsSingle();
        }

        private void BindStatesFactory()
        {
            Container.Bind<IStatesFactory>().To<StatesFactory>().AsSingle();
        }

        private void BindBuyAreaState()
        {
            Container.Bind<State>().To<BuyAreaState>().AsSingle()
                .WithArguments(StatesIds.BUY_AREA_STATE, buyAreaCameraId);
        }

        private void BindState<T>(string stateId)
            where T : State
        {
            Container.Bind<State>().To<T>().AsSingle().WithArguments(stateId);
        }

        public IEnumerable<string> GetCamerasIds()
        {
            if (camerasDatabase == null)
            {
                return null;
            }

            return new List<string>(camerasDatabase.Cameras.Keys);
        }
    }
}