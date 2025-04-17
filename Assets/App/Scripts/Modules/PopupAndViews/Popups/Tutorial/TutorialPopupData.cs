using System;
using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.Commands.General;

namespace App.Scripts.Modules.PopupAndViews.Popups.Tutorial
{
    [Serializable]
    public class TutorialPopupData
    {
        public string Header;
        public List<TutorialData> Tutorials;
        public ILabeledCommand Command;
        
    }
}