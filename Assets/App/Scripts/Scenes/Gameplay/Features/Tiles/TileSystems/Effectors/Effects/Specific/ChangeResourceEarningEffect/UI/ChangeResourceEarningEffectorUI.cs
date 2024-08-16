using App.Scripts.Modules.Localization.Localizers;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.Effectors.UI.ViewModels;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI;
using TMPro;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.Effectors.UI
{
    public class ChangeResourceEarningEffectorUI:SystemUI
    {
        [SerializeField] private TMProLocalizer description;
        
        private ChangeResourceEarningEffectorViewModule viewModule;

        public void Initialize(ChangeResourceEarningEffectorViewModule viewModule)
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

            description.Key = $"Accelerates resource extraction by {(viewModule.Effect.EarningAmountMultiplier - 1) * 100}%" ;
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