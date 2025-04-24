using App.Scripts.Modules.Saves;
using App.Scripts.Modules.StateMachine;
using App.Scripts.Modules.TasksSystem.Providers;
using App.Scripts.Scenes.Gameplay.Features.Commands.General;
using App.Scripts.Scenes.Gameplay.Features.Saves;
using App.Scripts.Scenes.MainMenu.StateMachines.Ids;
using UnityEngine;

namespace App.Scripts.Scenes.MainMenu.Commands
{
    public class NewGameCommand : LabeledCommand
    {
        private readonly StateMachine stateMachine;
        private readonly IDataProvider<GamePlaySavesData> dataProvider;
        private readonly IDataProvider<TasksData> taskdataProvider;
        
        public NewGameCommand(string label, StateMachine stateMachine,
            IDataProvider<GamePlaySavesData> dataProvider, IDataProvider<TasksData> taskdataProvider)
            : base(label)
        {
            this.stateMachine = stateMachine;
            this.dataProvider = dataProvider;
            this.taskdataProvider = taskdataProvider;
        }

        public override async void Execute()
        {
            taskdataProvider.DeleteData();
            dataProvider.DeleteData();
            await stateMachine.ChangeState(StatesIds.LOAD_SCENE_STATE);
        }
    }
}