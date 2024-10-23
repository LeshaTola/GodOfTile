using App.Scripts.Modules.StateMachine;
using App.Scripts.Scenes.Gameplay.Features.Commands.General;
using App.Scripts.Scenes.MainMenu.StateMachines.Ids;
using UnityEngine;

namespace App.Scripts.Scenes.MainMenu.Commands
{
    public class NewGameCommand : LabeledCommand
    {
        private StateMachine stateMachine;

        public NewGameCommand(string label, StateMachine stateMachine)
            : base(label)
        {
            this.stateMachine = stateMachine;
        }

        public override async void Execute()
        {
            Debug.Log("Delete All Saves, Load new Scene");
            await stateMachine.ChangeState(StatesIds.LOAD_SCENE_STATE);
        }
    }
}