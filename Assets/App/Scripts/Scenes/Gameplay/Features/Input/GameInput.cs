using System;
using App.Scripts.Modules.StateMachine.Services.CleanupService;
using App.Scripts.Scenes.Gameplay.Features.Grid;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace App.Scripts.Scenes.Gameplay.Features.Input
{
    public class GameInput : IGameInput, ICleanupable
    {
        public event Action OnBuild;
        public event Action OnRotate;

        private Camera mainCamera;
        private IGridProvider gridProvider;

        private global::Input input;
        private Plane ground;
        private Vector2Int lastPosition;

        public GameInput(Camera mainCamera, IGridProvider gridProvider)
        {
            this.mainCamera = mainCamera;

            input = new global::Input();
            input.Game.Enable();

            input.Game.Build.performed += BuildButtonPerformed;
            input.Game.Rotate.performed += RotationButtonPerformed;

            ground = new Plane(Vector2.up, Vector3.zero);
            this.gridProvider = gridProvider;
        }

        public Vector2 GetMoveVectorNormalized()
        {
            Vector2 moveVector;
            moveVector = input.Game.Move.ReadValue<Vector2>();
            return moveVector.normalized;
        }

        public float GetRotationValueNormalized()
        {
            return input.Game.Rotation.ReadValue<float>();
        }

        public void Cleanup()
        {
            input.Game.Build.performed -= BuildButtonPerformed;
        }

        public bool IsMouseClicked()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return false;
            }

            return Mouse.current.leftButton.wasPressedThisFrame;
        }

        public Vector2Int GetGridMousePosition()
        {
            var ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (
                !ground.Raycast(ray, out var position)
                || EventSystem.current.IsPointerOverGameObject()
            )
            {
                return lastPosition;
            }

            var worldPosition = ray.GetPoint(position);
            lastPosition = new Vector2Int(
                Mathf.RoundToInt(worldPosition.x),
                Mathf.RoundToInt(worldPosition.z)
            );

            return lastPosition;
        }

        private void BuildButtonPerformed(InputAction.CallbackContext obj)
        {
            OnBuild?.Invoke();
        }

        private void RotationButtonPerformed(InputAction.CallbackContext obj)
        {
            OnRotate?.Invoke();
        }
    }
}