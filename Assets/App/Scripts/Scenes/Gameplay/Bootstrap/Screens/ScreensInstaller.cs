using App.Scripts.Scenes.Gameplay.Features.Screens.Gameplay;
using App.Scripts.Scenes.Gameplay.Features.Screens.Gameplay.Presenters;
using App.Scripts.Scenes.Gameplay.Features.Screens.Gameplay.StateTransitioner;
using App.Scripts.Scenes.Gameplay.Features.Screens.Gameplay.StateTransitioner.Presenters;
using App.Scripts.Scenes.Gameplay.Features.Time.Configs;
using App.Scripts.Scenes.Gameplay.Features.Time.Controllers;
using App.Scripts.Scenes.Gameplay.Features.Time.UI;
using UnityEngine;
using Zenject;

namespace App.Scripts.Scenes.Gameplay.Bootstrap.Screens
{
    public class ScreensInstaller:MonoInstaller
    {
        [Header("GameplayScreen")]
        [SerializeField] private GameplayScreen gameplayScreen;
        
        [SerializeField] private StateTransferView stateTransferView;
        
        [Header("Time")]
        [SerializeField] private TimeControllerView timeControllerView;
        [SerializeField] private TimeSpeedConfig timeSpeedConfig;
        public override void InstallBindings()
        {
            BindGameplayScreen();
            BindTimeControllerView();
            BindStateTransferView();
        }

        private void BindGameplayScreen()
        {
            Container.Bind<GameplayScreen>().FromInstance(gameplayScreen).AsSingle();
            Container.Bind<GameplayScreenPresenter>().AsSingle();
        }
        
        private void BindTimeControllerView()
        {
            Container.Bind<TimePresenter>().AsSingle().WithArguments(timeSpeedConfig);
            Container.Bind<TimeControllerView>().FromInstance(timeControllerView).AsSingle();
        }

        private void BindStateTransferView()
        {
            Container.Bind<StateTransferView>().FromInstance(stateTransferView).AsSingle();
            Container.Bind<StateTransferPresenter>().AsSingle();
        }
    }
}