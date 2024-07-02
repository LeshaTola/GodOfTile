using Assets.App.Scripts.Features.Popups.InformationPopup.Routers;
using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Providers;
using Cysharp.Threading.Tasks;
using Features.StateMachineCore;
using Features.StateMachineCore.States;
using System.Collections.Generic;
using UnityEngine.InputSystem;

namespace Assets.App.Scripts.Scenes.Gameplay.StateMachines.States
{
	public class InformationState : State
	{
		private List<IUpdatable> updatables;
		private IInformationPopupRouter informationPopupRouter;
		private ITileSelectionProvider tileSelectionProvider;

		public InformationState(
			string id,
			List<IUpdatable> updatables,
			IInformationPopupRouter informationPopupRouter,
			ITileSelectionProvider tileSelectionProvider
		)
			: base(id)
		{
			this.updatables = updatables;
			this.informationPopupRouter = informationPopupRouter;
			this.tileSelectionProvider = tileSelectionProvider;
		}

		public override async void Enter()
		{
			base.Enter();
			await ShowTileInformation();
		}

		public override async void Update()
		{
			base.Update();

			foreach (var updatable in updatables)
			{
				updatable.Update();
			}

			if (Mouse.current.leftButton.wasPressedThisFrame)
			{
				await ShowTileInformation();
			}
		}

		public override async void Exit()
		{
			base.Exit();
			await informationPopupRouter.HideInformationPopup();
		}

		private async UniTask ShowTileInformation()
		{
			var tile = tileSelectionProvider.GetTileAtMousePosition();

			if (tile == null)
			{
				return;
			}

			await informationPopupRouter.ShowInformationPopup(tile.Config);
		}
	}
}
