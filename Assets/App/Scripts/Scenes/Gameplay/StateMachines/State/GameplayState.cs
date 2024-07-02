using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Providers;
using Features.StateMachineCore;
using Features.StateMachineCore.States;
using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace Assets.App.Scripts.Scenes.Gameplay.StateMachines.States
{
	public class GameplayState : State
	{
		private List<IUpdatable> updatables;
		private IGameInput gameInput;
		private ITileSelectionProvider tileSelectionProvider;

		public GameplayState(
			string id,
			List<IUpdatable> updatables,
			IGameInput gameInput,
			ITileSelectionProvider tileSelectionProvider
		)
			: base(id)
		{
			this.updatables = updatables;
			this.gameInput = gameInput;
			this.tileSelectionProvider = tileSelectionProvider;
		}

		public override void Enter()
		{
			base.Enter();

			gameInput.OnBuild += OnBuild;
		}

		public override void Update()
		{
			base.Update();

			foreach (var updatable in updatables)
			{
				updatable.Update();
			}

			if (Mouse.current.leftButton.wasPressedThisFrame)
			{
				var tile = tileSelectionProvider.GetTileAtMousePosition();

				if (tile == null)
				{
					return;
				}

				StateMachine.ChangeState(StatesIds.INFORMATION_STATE);
			}
		}

		public override void Exit()
		{
			base.Exit();

			gameInput.OnBuild -= OnBuild;
		}

		private void OnBuild()
		{
			StateMachine.ChangeState(StatesIds.BUILDING_STATE);
		}
	}
}
