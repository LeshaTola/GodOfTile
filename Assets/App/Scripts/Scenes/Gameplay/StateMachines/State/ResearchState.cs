using App.Scripts.Modules.StateMachine.Services.UpdateService;
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
        private readonly IUpdateService updateService;
        private ResearchPopupRouter researchPopupRouter;

        public ResearchState(string id,
            IGameInput gameInput,
            ResearchPopupRouter researchPopupRouter, 
            ICommandsProvider commandsProvider,
            IUpdateService updateService) : base(id)
        {
            this.researchPopupRouter = researchPopupRouter;
            this.gameInput = gameInput;
            this.commandsProvider = commandsProvider;
            this.updateService = updateService;
        }

        public override async UniTask Enter()
        {
            await base.Enter();
            await researchPopupRouter.ShowPopup();

            gameInput.OnEscape += Back;
            gameInput.OnM += Back;
        }

        public override async UniTask Update()
        {
            await base.Update();

            updateService.Update();
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