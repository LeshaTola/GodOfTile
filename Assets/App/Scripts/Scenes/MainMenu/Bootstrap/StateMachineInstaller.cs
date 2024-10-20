using System.Collections.Generic;
using App.Scripts.Modules.CameraSwitchers;
using App.Scripts.Modules.StateMachine;
using App.Scripts.Modules.StateMachine.Factories.States;
using App.Scripts.Modules.StateMachine.States.General;
using App.Scripts.Scenes.Gameplay.Features.Shop.Configs;
using App.Scripts.Scenes.MainMenu.StateMachines.Ids;
using App.Scripts.Scenes.MainMenu.StateMachines.States;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace App.Scripts.Scenes.MainMenu.Bootstrap
{
    public class StateMachineInstaller : MonoInstaller
    {
        [SerializeField] private CamerasDatabase camerasDatabase;
        [SerializeField] private CollectionConfig startTiles;

        [ValueDropdown(nameof(GetCamerasIds))]
        [SerializeField] private string mainCameraId;

        [ValueDropdown(nameof(GetCamerasIds))]
        [SerializeField] private string InfoCameraId;

        public override void InstallBindings()
        {
            BindStatesFactory();
            BindStateMachine();

            BindInitialState();
            Container.Bind<State>().To<MainState>().AsSingle()
                .WithArguments(StatesIds.MAIN_STATE, startTiles);
        }

        private void BindInitialState()
        {
            Container.Bind<State>().To<InitialState>().AsSingle()
                .WithArguments(StatesIds.INITIAL_STATE, mainCameraId);
        }

        private void BindStateMachine()
        {
            Container.Bind<StateMachine>().AsSingle();
        }

        private void BindStatesFactory()
        {
            Container.Bind<IStatesFactory>().To<StatesFactory>().AsSingle();
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