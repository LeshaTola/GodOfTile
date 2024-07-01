using System.Collections.Generic;
using Assets.App.Scripts.Features.Popups.InformationPopup.Routers;
using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Providers;
using Features.StateMachineCore;
using Features.StateMachineCore.States;
using UnityEngine;
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

        public override void Enter()
        {
            base.Enter();
            ShowTileInformation();
            Debug.Log("InformationState");
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
                ShowTileInformation();
            }
        }

        public override void Exit()
        {
            base.Exit();
            informationPopupRouter.HideInformationPopup();
        }

        private void ShowTileInformation()
        {
            var tile = tileSelectionProvider.GetTileAtMousePosition();

            if (tile == null)
            {
                return;
            }

            informationPopupRouter.ShowInformationPopup(tile.Config);
        }
    }
}
