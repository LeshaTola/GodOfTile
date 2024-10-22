using App.Scripts.Modules.Localization.Localizers;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI;
using UnityEngine;

namespace App.Scripts.Features.Tiles.Systems.Views.OnlyText
{
    public class OnlyTextSystemSystemUI : SystemUI
    {
        [SerializeField] private TMPLocalizer text;

        private OnlyTextSystemSystemViewModel viewModule;

        public void Initialize(OnlyTextSystemSystemViewModel viewModule)
        {
            Cleanup();
            this.viewModule = viewModule;

            text.Initialize(viewModule.LocalizationSystem);

            Setup();
        }

        public override void Setup()
        {
            if (viewModule == null)
            {
                return;
            }

            text.Key = viewModule.Data;

            text.Translate();
        }

        public override void Cleanup()
        {
            viewModule = null;
        }
    }
}