using App.Scripts.Modules.Localization.Elements.Buttons;
using App.Scripts.Modules.Localization.Localizers;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI;
using UnityEngine;

namespace App.Scripts.Features.Tiles.Systems.Views.TwoButtons
{
    public class TwoButtonsSystemUI :SystemUI
    {
        [SerializeField] private TMPLocalizer text;
        [SerializeField] private TMPLocalizedButton yesButton;
        [SerializeField] private TMPLocalizedButton noButton;
        
        private TwoButtonsSystemUIViewModule viewModule;
        
        public void Initialize(TwoButtonsSystemUIViewModule viewModule)
        {
            Cleanup();
            this.viewModule = viewModule;
            
            text.Initialize(viewModule.LocalizationSystem);
            yesButton.Initialize(viewModule.LocalizationSystem);
            noButton.Initialize(viewModule.LocalizationSystem);
        }

        public override void Setup()
        {
            if (viewModule == null)
            {
                return;
            }

            text.Key = viewModule.Text;
            text.Translate();
            
            yesButton.UpdateAction(viewModule.YesAction.Execute);
            yesButton.UpdateText(viewModule.YesAction.Label);
            yesButton.Translate();
            
            noButton.UpdateAction(viewModule.NoAction.Execute);
            noButton.UpdateText(viewModule.NoAction.Label);
            noButton.Translate();
        }

        public override void Cleanup()
        {
            if (viewModule == null)
            {
                return;
            }
            
            viewModule = null;
            yesButton.Cleanup();
            noButton.Cleanup();
        }
    }
}