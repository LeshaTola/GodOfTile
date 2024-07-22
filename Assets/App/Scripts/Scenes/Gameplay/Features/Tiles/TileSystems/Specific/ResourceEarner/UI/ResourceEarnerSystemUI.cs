using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI.Specific.ResourceEarner.ViewModels;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI.Specific.ResourceEarner
{
    public class ResourceEarnerSystemUI : SystemUI
    {
        private IResourceEarnerViewModule viewModule;

        public void Init(IResourceEarnerViewModule viewModule)
        {
            this.viewModule = viewModule;
        }

        public override void Show()
        {
            if (viewModule == null)
            {
                return;
            }
        }

        public override void Translate()
        {
            throw new System.NotImplementedException();
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