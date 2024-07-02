using Assets.App.Scripts.Scenes.Gameplay.Features.Creation.Services;
using Features.StateMachineCore;
using Features.StateMachineCore.States;
using System.Collections.Generic;

namespace Assets.App.Scripts.Scenes.Gameplay.StateMachines.States
{
	public class BuildState : State
	{
		private List<IUpdatable> updatables;
		private IGameInput gameInput;
		private ITilesCreationService creationService;

		public BuildState(
			string id,
			List<IUpdatable> updatables,
			IGameInput gameInput,
			ITilesCreationService creationService
		)
			: base(id)
		{
			this.updatables = updatables;
			this.gameInput = gameInput;
			this.creationService = creationService;
		}

		public override void Enter()
		{
			base.Enter();

			gameInput.OnBuild += OnBuild;
			gameInput.OnRotate += OnRotate;

			creationService.StartPlacingTile();
		}

		public override void Update()
		{
			base.Update();

			foreach (var updatable in updatables)
			{
				updatable.Update();
			}

			creationService.MoveActiveTile(gameInput.GetGroundMousePosition());

			if (gameInput.IsMouseClicked())
			{
				creationService.PlaceActiveTile();
				creationService.StartPlacingTile();
			}
		}

		public override void Exit()
		{
			base.Exit();

			creationService.StopPlacingTile();

			gameInput.OnBuild -= OnBuild;
			gameInput.OnRotate -= OnRotate;
		}

		private void OnBuild()
		{
			StateMachine.ChangeState(StatesIds.GAMEPLAY_STATE);
		}

		private void OnRotate()
		{
			creationService.RotateActiveTile();
		}
	}
}
