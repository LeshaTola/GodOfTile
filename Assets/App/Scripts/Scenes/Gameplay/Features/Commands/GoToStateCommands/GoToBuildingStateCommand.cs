using App.Scripts.Modules.StateMachine;
using App.Scripts.Scenes.Gameplay.Features.Commands.General;
using App.Scripts.Scenes.Gameplay.Features.Researches.Services;
using App.Scripts.Scenes.Gameplay.StateMachines.Ids;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Commands.GoToStateCommands
{
    public class GoToBuildingStateCommand : LabeledCommand
    {
        private StateMachine stateMachine;

        public GoToBuildingStateCommand(string label, StateMachine stateMachine)
            : base(label)
        {
            this.stateMachine = stateMachine;
        }

        public override async void Execute()
        {
            await stateMachine.ChangeState(StatesIds.BUILDING_STATE);
        }
    }

    public class GoToResearchStateCommand : LabeledCommand
    {
        private StateMachine stateMachine;
        private IResearchService researchService;

        public GoToResearchStateCommand(string label, StateMachine stateMachine, IResearchService researchService)
            : base(label)
        {
            this.stateMachine = stateMachine;
            this.researchService = researchService;
        }

        public override async void Execute()
        {
            if (researchService.ResearchSystems.Count <= 0)
            {
                Debug.Log("Not Enough Research Systems");
                return;
            }

            await stateMachine.ChangeState(StatesIds.RESEARCH_STATE);
        }
    }
}