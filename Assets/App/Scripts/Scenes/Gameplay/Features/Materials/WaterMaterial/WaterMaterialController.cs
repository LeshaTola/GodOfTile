using App.Scripts.Scenes.Gameplay.Features.Materials.WaterMaterial.Configs;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Materials.WaterMaterial
{
    public class WaterMaterialController : IWaterMaterialController
    {
        private WaterMaterialConfig config;

        public WaterMaterialController(WaterMaterialConfig config)
        {
            this.config = config;
        }

        public void SetWaterSpeed(float multiplier)
        {
            config.Material.SetVector("_SurfaceNoiseScroll", config.StartSpeed * multiplier);
        }
    }
}