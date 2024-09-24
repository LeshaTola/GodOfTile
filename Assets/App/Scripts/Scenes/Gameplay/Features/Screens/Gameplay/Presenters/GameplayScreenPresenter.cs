using App.Scripts.Modules.StateMachine.Services.CleanupService;
using App.Scripts.Modules.StateMachine.Services.InitializeService;
using App.Scripts.Scenes.Gameplay.Features.Screens.Gameplay.StateTransfer.Presenters;
using App.Scripts.Scenes.Gameplay.Features.Screens.Gameplay.TileInformation.Presenters;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using App.Scripts.Scenes.Gameplay.Features.Time.Presenters;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Scenes.Gameplay.Features.Screens.Gameplay.Presenters
{
    public class GameplayScreenPresenter : IInitializable, ICleanupable
    {
        private GameplayScreen gameplayScreen;
        private TimePresenter timePresenter;
        private StateTransferPresenter stateTransferPresenter;
        private TileInformationPresenter tileInformationPresenter;

        public GameplayScreenPresenter(
            GameplayScreen gameplayScreen,
            TimePresenter timePresenter,
            StateTransferPresenter stateTransferPresenter,
            TileInformationPresenter tileInformationPresenter)
        {
            this.gameplayScreen = gameplayScreen;
            this.timePresenter = timePresenter;
            this.stateTransferPresenter = stateTransferPresenter;
            this.tileInformationPresenter = tileInformationPresenter;
        }

        public void Initialize()
        {
            timePresenter.Initialize();
            stateTransferPresenter.Initialize();
            tileInformationPresenter.Initialize();
        }

        public void Cleanup()
        {
            timePresenter.Cleanup();
            stateTransferPresenter.Cleanup();
            tileInformationPresenter.Cleanup();
        }

        public async UniTask ShowTileInformation(TileConfig tileConfig)
        {
            tileInformationPresenter.Setup(tileConfig);
            await tileInformationPresenter.Show();
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
            var tileInformationTask = tileInformationPresenter.Hide();
            
            await UniTask.WhenAll(timeTask, stateTransferTask,tileInformationTask);
            await gameplayScreen.Hide();
        }
    }
}