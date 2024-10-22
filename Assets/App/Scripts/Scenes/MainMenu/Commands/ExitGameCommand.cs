using App.Scripts.Scenes.Gameplay.Features.Commands.General;
using UnityEngine;

namespace App.Scripts.Scenes.MainMenu.Commands
{
    public class ExitGameCommand:LabeledCommand
    {
        public ExitGameCommand(string label) : base(label)
        {
        }

        public override void Execute()
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
