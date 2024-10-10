using App.Scripts.Modules.Localization.Localizers;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.Effects.Specific.ChangeResourceEarningEffect.UI.
    ViewModels;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.Effects.Specific.ChangeResourceEarningEffect.
    UI
{
    public class CollectTilesAroundffectorUI : SystemUI
    {
        [SerializeField] private TMPLocalizer description;

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

            description.Key =
                $"Accelerates resource extraction by {(viewModule.Effect.EarningAmountMultiplier - 1) * 100}%";
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