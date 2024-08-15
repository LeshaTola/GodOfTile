using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.ResourceEarners.UI.ViewModels;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.ResourceEarners.UI
{
    public class ResourceEarnerUI : SystemUI
    {
        [SerializeField] private ResourceInfoUI resourceInfoUI;
        [SerializeField] private string postInfoText;

        private IResourceEarnerViewModule viewModule;

        public void Initialize(IResourceEarnerViewModule viewModule)
        {
            this.viewModule = viewModule;
            resourceInfoUI.Initialize(viewModule.LocalizationSystem);
        }

        public override void Setup()
        {
            if (viewModule == null)
            {
                return;
            }

            var info = viewModule.SystemData.AmountPerSecond.ToString() + postInfoText;
            resourceInfoUI.Setup(viewModule.SystemData.Resource, info);
            resourceInfoUI.Translate();
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