using App.Scripts.Modules.TimeProvider;
using App.Scripts.Scenes.Gameplay.Features.Materials.WaterMaterial;

namespace App.Scripts.Scenes.Gameplay.Features.Time.Services.TimeServices
{
    public class TimeService : ITimeService
    {
        private ITimeProvider timeProvider;
        private IWaterMaterialController waterMaterialController;

        public TimeService(ITimeProvider timeProvider, IWaterMaterialController waterMaterialController)
        {
            this.timeProvider = timeProvider;
            this.waterMaterialController = waterMaterialController;
        }

        public void SetPause()
        {
            timeProvider.TimeMultiplier = 0;
            waterMaterialController.SetWaterSpeed(0);
        }

        public void SetSpeed(float speed)
        {
            timeProvider.TimeMultiplier = speed;
            waterMaterialController.SetWaterSpeed(speed);
        }
    }
}