using App.Scripts.Modules.PopupLogic.General.Controllers;
using App.Scripts.Scenes.Gameplay.Features.Commands.General;

namespace App.Scripts.Scenes.Gameplay.Features.Commands
{
    public class ClosePopupCommand : LabeledCommand
    {
        private IPopupController popupController;

        public ClosePopupCommand(string label, IPopupController popupController)
            : base(label)
        {
            this.popupController = popupController;
        }

        public override void Execute()
        {
            popupController.HideLastPopup();
        }
    }
}