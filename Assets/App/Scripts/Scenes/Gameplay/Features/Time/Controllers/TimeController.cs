using App.Scripts.Modules.StateMachine.Services.CleanupService;
using App.Scripts.Modules.StateMachine.Services.InitializeService;
using App.Scripts.Scenes.Gameplay.Features.Input;
using App.Scripts.Scenes.Gameplay.Features.Time.Services.TimeServices;
using App.Scripts.Scenes.Gameplay.Features.Time.UI;
using UnityEngine.UI;

namespace App.Scripts.Scenes.Gameplay.Features.Time.Controllers
{
    public class TimeController : IInitializable, ICleanupable
    {
        private IGameInput gameInput;
        private ITimeService timeService;
        private TimeControllerUI timeControllerUI;
        private TimeSpeedConfig config;

        public TimeController(IGameInput gameInput, ITimeService timeService, TimeControllerUI timeControllerUI,
            TimeSpeedConfig config)
        {
            this.gameInput = gameInput;
            this.timeService = timeService;
            this.timeControllerUI = timeControllerUI;
            this.config = config;
            
            SetSpeed1();
        }

        public void Initialize()
        {
            gameInput.OnPause += SetPause;
            gameInput.OnSpeed1 += SetSpeed1;
            gameInput.OnSpeed2 += SetSpeed2;
            gameInput.OnSpeed3 += SetSpeed3;

            timeControllerUI.OnPauseButtonClicked += SetPause;
            timeControllerUI.OnSpeed1ButtonClicked += SetSpeed1;
            timeControllerUI.OnSpeed2ButtonClicked += SetSpeed2;
            timeControllerUI.OnSpeed3ButtonClicked += SetSpeed3;
        }

        public void Cleanup()
        {
            gameInput.OnPause -= SetPause;
            gameInput.OnSpeed1 -= SetSpeed1;
            gameInput.OnSpeed2 -= SetSpeed2;
            gameInput.OnSpeed3 -= SetSpeed3;

            timeControllerUI.OnPauseButtonClicked -= SetPause;
            timeControllerUI.OnSpeed1ButtonClicked -= SetSpeed1;
            timeControllerUI.OnSpeed2ButtonClicked -= SetSpeed2;
            timeControllerUI.OnSpeed3ButtonClicked -= SetSpeed3;
        }

        private void SetPause()
        {
            timeService.SetPause();
            timeControllerUI.SetSelector(timeControllerUI.PauseButton);
        }

        private void SetSpeed1()
        {
            timeService.SetSpeed(config.Speed1);
            timeControllerUI.SetSelector(timeControllerUI.Speed1Button);

        }

        private void SetSpeed2()
        {
            timeService.SetSpeed(config.Speed2);
            timeControllerUI.SetSelector(timeControllerUI.Speed2Button);
        }

        private void SetSpeed3()
        {
            timeService.SetSpeed(config.Speed3);
            timeControllerUI.SetSelector(timeControllerUI.Speed3Button);
        }
    }
}