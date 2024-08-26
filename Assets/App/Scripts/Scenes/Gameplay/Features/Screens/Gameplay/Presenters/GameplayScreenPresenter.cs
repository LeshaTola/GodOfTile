using App.Scripts.Modules.StateMachine.Services.CleanupService;
using App.Scripts.Modules.StateMachine.Services.InitializeService;
using App.Scripts.Scenes.Gameplay.Features.Screens.Gameplay.StateTransitioner.Presenters;
using App.Scripts.Scenes.Gameplay.Features.Time.Controllers;

namespace App.Scripts.Scenes.Gameplay.Features.Screens.Gameplay.Presenters
{
    public class GameplayScreenPresenter : IInitializable , ICleanupable
    {
        private GameplayScreen gameplayScreen;
        private TimePresenter timePresenter;
        private StateTransferPresenter stateTransferPresenter;

        public GameplayScreenPresenter(
            GameplayScreen gameplayScreen,
            TimePresenter timePresenter,
            StateTransferPresenter stateTransferPresenter)
        {
            this.gameplayScreen = gameplayScreen;
            this.timePresenter = timePresenter;
            this.stateTransferPresenter = stateTransferPresenter;
        }

        public void Initialize()
        {
            timePresenter.Initialize();
            stateTransferPresenter.Initialize();
            
            gameplayScreen.Show();
        }

        public void Cleanup()
        {
            timePresenter.Cleanup();
            stateTransferPresenter.Cleanup();

            gameplayScreen.Hide();
        }
    }
}