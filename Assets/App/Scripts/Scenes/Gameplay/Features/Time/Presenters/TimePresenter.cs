using App.Scripts.Modules.Sounds;
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
        private TimeControllerView view;
        private readonly ISoundProvider soundProvider;
        private TimeSpeedConfig config;

        public TimePresenter(IGameInput gameInput,
            ITimeService timeService,
            TimeControllerView view,
            ISoundProvider soundProvider,
            TimeSpeedConfig config)
        {
            this.gameInput = gameInput;
            this.timeService = timeService;
            this.view = view;
            this.soundProvider = soundProvider;
            this.config = config;
        }

        public void Initialize()
        {
            view.Initialize();

            gameInput.OnPause += SetPause;
            gameInput.OnSpeed1 += SetSpeed1;
            gameInput.OnSpeed2 += SetSpeed2;
            gameInput.OnSpeed3 += SetSpeed3;

            view.OnPauseButtonClicked += SetPause;
            view.OnSpeed1ButtonClicked += SetSpeed1;
            view.OnSpeed2ButtonClicked += SetSpeed2;
            view.OnSpeed3ButtonClicked += SetSpeed3;

            SetSpeed1();
        }

        public void Cleanup()
        {
            view.Cleanup();

            gameInput.OnPause -= SetPause;
            gameInput.OnSpeed1 -= SetSpeed1;
            gameInput.OnSpeed2 -= SetSpeed2;
            gameInput.OnSpeed3 -= SetSpeed3;

            view.OnPauseButtonClicked -= SetPause;
            view.OnSpeed1ButtonClicked -= SetSpeed1;
            view.OnSpeed2ButtonClicked -= SetSpeed2;
            view.OnSpeed3ButtonClicked -= SetSpeed3;
        }

        private void SetPause()
        {
            timeService.SetPause();
            view.SetSelector(view.PauseButton);
            soundProvider.PlaySound(view.ButtonSoundKey);
        }

        private void SetSpeed1()
        {
            timeService.SetSpeed(config.Speed1);
            view.SetSelector(view.Speed1Button);
            soundProvider.PlaySound(view.ButtonSoundKey);
        }

        private void SetSpeed2()
        {
            timeService.SetSpeed(config.Speed2);
            view.SetSelector(view.Speed2Button);
            soundProvider.PlaySound(view.ButtonSoundKey);
        }

        private void SetSpeed3()
        {
            timeService.SetSpeed(config.Speed3);
            view.SetSelector(view.Speed3Button);
            soundProvider.PlaySound(view.ButtonSoundKey);
        }

        public async UniTask Show()
        {
            await view.Show();
        }

        public async UniTask Hide()
        {
            await view.Hide();
        }
    }
}