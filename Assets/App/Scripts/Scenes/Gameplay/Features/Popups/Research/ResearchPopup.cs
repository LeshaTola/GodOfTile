using App.Scripts.Modules.PopupLogic.General.Popup;
using App.Scripts.Scenes.Gameplay.Features.Popups.Research.ViewModels;

namespace App.Scripts.Scenes.Gameplay.Features.Popups.Research
{
    public class ResearchPopup:Popup
    {
        
        
        private ResearchPopupViewModule viewModule;

        public void Setup(ResearchPopupViewModule viewModule)
        {
            Cleanup();
            
            this.viewModule = viewModule;
            
            Initialize();
            SetupUI();
            Translate();
        }

        public void Initialize()
        {
            
        }
        
        public void SetupUI()
        {
            
        }
        
        public void Translate()
        {
            
        }
        
        public void Cleanup()
        {
            if (viewModule == null)
            {
                return;
            }

            viewModule = null;
        }
    }
}