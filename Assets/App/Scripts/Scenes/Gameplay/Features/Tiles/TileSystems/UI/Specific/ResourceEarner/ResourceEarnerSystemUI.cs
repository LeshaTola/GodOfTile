using App.Scripts.Modules.Localization;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI.Specific
{
    public class ResourceEarnerSystemUI : SystemUI
    {
        
        private TileSystem tileSystem;

        public override void Init(TileSystem system, ILocalizationSystem localizationSystem)
        {
            tileSystem = system;
        }

        public override void Translate()
        {
            throw new System.NotImplementedException();
        }

        public override void Cleanup()
        {
            if (tileSystem == null)
            {
                return;
            }

            tileSystem = null;
        }
    }
}