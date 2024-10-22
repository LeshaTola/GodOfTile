using App.Scripts.Scenes.Gameplay.Features.Commands.General;
using UnityEngine;

namespace App.Scripts.Scenes.MainMenu.Commands
{
    public class NewGameCommand : LabeledCommand
    {
        public NewGameCommand(string label) : base(label)
        {
        }

        public override void Execute()
        {
            Debug.Log("Delete All Saves, Load new Scene");
        }
    }
}