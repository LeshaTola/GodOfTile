using App.Scripts.Scenes.Gameplay.Features.Commands.GoToStateCommands;
using App.Scripts.Scenes.Gameplay.Features.Commands.Provider;
using App.Scripts.Scenes.Gameplay.Features.Input;
using App.Scripts.Scenes.Gameplay.Features.Popups.Research.Routers;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Scenes.Gameplay.StateMachines.State
{
    public class ResearchState : Modules.StateMachine.States.General.State
    {
        private IGameInput gameInput;
        private ICommandsProvider commandsProvider;
        private ResearchPopupRouter researchPopupRouter;

        public ResearchState(string id, ResearchPopupRouter researchPopupRouter, IGameInput gameInput,
            ICommandsProvider commandsProvider) : base(id)
        {
            this.researchPopupRouter = researchPopupRouter;
            this.gameInput = gameInput;
            this.commandsProvider = commandsProvider;
        }

        public override async UniTask Enter()
        {
            await base.Enter();
            await researchPopupRouter.ShowPopup();

            gameInput.OnEscape += Back;
            gameInput.OnM += Back;
        }

        public override async UniTask Exit()
        {
            await base.Exit();
            
            gameInput.OnEscape -= Back;
            gameInput.OnM -= Back;
            
            await researchPopupRouter.HidePopup();
        }

        private void Back()
        {
            commandsProvider.GetCommand<GoToGamePlayStateCommand>().Execute();
        }
    }
}