using App.Scripts.Modules.StateMachine.Services.CleanupService;
using App.Scripts.Modules.StateMachine.Services.InitializeService;
using App.Scripts.Scenes.Gameplay.Features.Input;
using App.Scripts.Scenes.Gameplay.Features.Time.Configs;
using App.Scripts.Scenes.Gameplay.Features.Time.Services.TimeServices;
using App.Scripts.Scenes.Gameplay.Features.Time.UI;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Scenes.Gameplay.Features.Time.Presenters
{
    public class TimePresenter : IInitializable, ICleanupable
    {
        private IGameInput gameInput;
        private ITimeService timeService;
        private TimeControllerView timeControllerView;
        private TimeSpeedConfig config;

        public TimePresenter(IGameInput gameInput,
            ITimeService timeService,
            TimeControllerView timeControllerView,
            TimeSpeedConfig config)
        {
            this.gameInput = gameInput;
            this.timeService = timeService;
            this.timeControllerView = timeControllerView;
            this.config = config;
        }

        public void Initialize()
        {
            timeControllerView.Initialize();

            gameInput.OnPause += SetPause;
            gameInput.OnSpeed1 += SetSpeed1;
            gameInput.OnSpeed2 += SetSpeed2;
            gameInput.OnSpeed3 += SetSpeed3;

            timeControllerView.OnPauseButtonClicked += SetPause;
            timeControllerView.OnSpeed1ButtonClicked += SetSpeed1;
            timeControllerView.OnSpeed2ButtonClicked += SetSpeed2;
            timeControllerView.OnSpeed3ButtonClicked += SetSpeed3;

            SetSpeed1();
        }

        public void Cleanup()
        {
            timeControllerView.Cleanup();

            gameInput.OnPause -= SetPause;
            gameInput.OnSpeed1 -= SetSpeed1;
            gameInput.OnSpeed2 -= SetSpeed2;
            gameInput.OnSpeed3 -= SetSpeed3;

            timeControllerView.OnPauseButtonClicked -= SetPause;
            timeControllerView.OnSpeed1ButtonClicked -= SetSpeed1;
            timeControllerView.OnSpeed2ButtonClicked -= SetSpeed2;
            timeControllerView.OnSpeed3ButtonClicked -= SetSpeed3;
        }

        private void SetPause()
        {
            timeService.SetPause();
            timeControllerView.SetSelector(timeControllerView.PauseButton);
        }

        private void SetSpeed1()
        {
            timeService.SetSpeed(config.Speed1);
            timeControllerView.SetSelector(timeControllerView.Speed1Button);
        }

        private void SetSpeed2()
        {
            timeService.SetSpeed(config.Speed2);
            timeControllerView.SetSelector(timeControllerView.Speed2Button);
        }

        private void SetSpeed3()
        {
            timeService.SetSpeed(config.Speed3);
            timeControllerView.SetSelector(timeControllerView.Speed3Button);
        }

        public async UniTask Show()
        {
            await timeControllerView.Show();
        }

        public async UniTask Hide()
        {
            await timeControllerView.Hide();
        }
    }
}