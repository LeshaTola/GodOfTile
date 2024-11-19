using App.Scripts.Modules.Saves;
using App.Scripts.Modules.StateMachine;
using App.Scripts.Scenes.Gameplay.Features.Commands.General;
using App.Scripts.Scenes.Gameplay.Features.Saves;
using App.Scripts.Scenes.MainMenu.StateMachines.Ids;
using UnityEngine;

namespace App.Scripts.Scenes.MainMenu.Commands
{
    public class NewGameCommand : LabeledCommand
    {
        private StateMachine stateMachine;
        private IDataProvider<GamePlaySavesData> dataProvider;
        
        public NewGameCommand(string label, StateMachine stateMachine,IDataProvider<GamePlaySavesData> dataProvider)
            : base(label)
        {
            this.stateMachine = stateMachine;
            this.dataProvider = dataProvider;
        }

        public override async void Execute()
        {
            dataProvider.DeleteData();
            await stateMachine.ChangeState(StatesIds.LOAD_SCENE_STATE);
        }
    }
}