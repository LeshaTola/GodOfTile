using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.Commands.General;
using UnityEngine;

namespace App.Scripts.Modules.PopupAndViews.Popups.Info
{
    public class InfoPopupData
    {
        public string Header;
        public string Mesage;
        public ILabeledCommand Command;
    }
}