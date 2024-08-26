using App.Scripts.Modules.Localization.Localizers;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.Research.UI.ViewModels;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.Research.UI
{
    public class ResearchSystemUI : SystemUI
    {
        [SerializeField]
        private TMProLocalizer text;
    
        private ResearchSystemViewModel viewModule;

        public void Initialize(ResearchSystemViewModel viewModule)
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

            text.Key = viewModule.Data.Description;
            
            text.Translate();
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