using Assets.App.Scripts.Scenes.Gameplay.Features.Commands.General;
using Module.PopupLogic.General.Controller;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Commands
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
