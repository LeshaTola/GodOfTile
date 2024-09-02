using App.Scripts.Modules.StateMachine.Services.CleanupService;
using App.Scripts.Modules.StateMachine.Services.InitializeService;
using App.Scripts.Scenes.Gameplay.Features.Screens.Gameplay.StateTransfer.Presenters;
using App.Scripts.Scenes.Gameplay.Features.Time.Presenters;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Scenes.Gameplay.Features.Screens.Gameplay.Presenters
{
    public class GameplayScreenPresenter : IInitializable, ICleanupable
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
        }

        public void Cleanup()
        {
            timePresenter.Cleanup();
            stateTransferPresenter.Cleanup();
        }

        public async UniTask Show()
        {
            await gameplayScreen.Show();
            var timeTask = timePresenter.Show();
            var stateTransferTask = stateTransferPresenter.Show();
            await UniTask.WhenAll(timeTask, stateTransferTask);
        }

        public async UniTask Hide()
        {
            var timeTask = timePresenter.Hide();
            var stateTransferTask = stateTransferPresenter.Hide();
            await UniTask.WhenAll(timeTask, stateTransferTask);
            await gameplayScreen.Hide();
        }
    }
}