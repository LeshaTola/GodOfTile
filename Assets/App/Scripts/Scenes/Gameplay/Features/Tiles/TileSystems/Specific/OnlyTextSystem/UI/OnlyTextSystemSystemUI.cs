using App.Scripts.Modules.Localization.Localizers;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.OnlyTextSystem.UI.ViewModels;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.OnlyTextSystem.UI
{
    public class OnlyTextSystemSystemUI : SystemUI
    {
        [SerializeField] private TMProLocalizer text;
    
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