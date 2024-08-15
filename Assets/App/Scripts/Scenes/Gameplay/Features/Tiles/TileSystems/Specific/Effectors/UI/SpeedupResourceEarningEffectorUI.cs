using App.Scripts.Modules.Localization.Localizers;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.Effectors.UI.ViewModels;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI;
using TMPro;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.Effectors.UI
{
    public class SpeedupResourceEarningEffectorUI:SystemUI
    {
        [SerializeField] private TMProLocalizer description;
        
        private SpeedupResourceEarningEffectorViewModule viewModule;

        public void Initialize(SpeedupResourceEarningEffectorViewModule viewModule)
        {
            this.viewModule = viewModule;
            description.Initialize(viewModule.LocalizationSystem);
        }
        
        public override void Setup()
        {
            if (viewModule == null)
            {
                return;
            }

            description.Key = $"Accelerates resource extraction by {(viewModule.Data.EarningAmountMultiplier - 1) * 100}%" ;
            description.Translate();
        }

        public override void Cleanup()
        {
            if (viewModule == null)
            {
                return;
            }

            viewModule = null;
        }
    }
}