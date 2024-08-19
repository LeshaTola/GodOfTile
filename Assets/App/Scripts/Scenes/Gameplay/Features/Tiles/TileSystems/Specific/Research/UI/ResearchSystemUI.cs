using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.Research.UI.ViewModels;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.Research.UI
{
    public class ResearchSystemUI:SystemUI
    {
        private ResearchSystemViewModel viewModule;

        public void Initialize(ResearchSystemViewModel viewModule)
        {
            Cleanup();
            this.viewModule = viewModule;
            Setup();
        }
        
        public override void Setup()
        {
            if (viewModule == null)
            {
                return;
            }
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