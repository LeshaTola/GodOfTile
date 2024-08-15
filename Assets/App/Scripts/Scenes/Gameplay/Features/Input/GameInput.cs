using System;
using App.Scripts.Modules.StateMachine.Services.CleanupService;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace App.Scripts.Scenes.Gameplay.Features.Input
{
    public class GameInput : IGameInput, ICleanupable
    {
        public event Action OnEscape;
        public event Action OnBuild;
        public event Action OnRotate;
        public event Action OnPause;
        public event Action OnSpeed1;
        public event Action OnSpeed2;
        public event Action OnSpeed3;
        

        private Camera mainCamera;

        private global::Input input;
        private Plane ground;
        private Vector2Int lastPosition;

        public GameInput(Camera mainCamera)
        {
            this.mainCamera = mainCamera;

            input = new global::Input();
            input.Game.Enable();

            input.Game.ESC.performed += OnESCPerformed;
            input.Game.Build.performed += BuildButtonPerformed;
            input.Game.Rotate.performed += RotationButtonPerformed;
            
            input.Game.Pause.performed += PauseButtonPerformed;
            input.Game.Speed1.performed += Speed1ButtonPerformed;
            input.Game.Speed2.performed += Speed2ButtonPerformed;
            input.Game.Speed3.performed += Speed3ButtonPerformed;

            ground = new Plane(Vector2.up, Vector3.zero);
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
            input.Game.ESC.performed -= OnESCPerformed;
            input.Game.Build.performed -= BuildButtonPerformed;
            input.Game.Rotate.performed -= RotationButtonPerformed;
            
            input.Game.Pause.performed -= PauseButtonPerformed;
            input.Game.Speed1.performed -= Speed1ButtonPerformed;
            input.Game.Speed2.performed -= Speed2ButtonPerformed;
            input.Game.Speed3.performed -= Speed3ButtonPerformed;
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

        private void OnESCPerformed(InputAction.CallbackContext obj)
        {
            OnEscape?.Invoke();
        }

        private void BuildButtonPerformed(InputAction.CallbackContext obj)
        {
            OnBuild?.Invoke();
        }

        private void RotationButtonPerformed(InputAction.CallbackContext obj)
        {
            OnRotate?.Invoke();
        }

        private void Speed1ButtonPerformed(InputAction.CallbackContext obj)
        {
            OnSpeed1?.Invoke();
        }
        
        private void Speed2ButtonPerformed(InputAction.CallbackContext obj)
        {
            OnSpeed2?.Invoke();

        }
        
        private void Speed3ButtonPerformed(InputAction.CallbackContext obj)
        {
            OnSpeed3?.Invoke();

        }

        private void PauseButtonPerformed(InputAction.CallbackContext obj)
        {
            OnPause?.Invoke();

        }
    }
}