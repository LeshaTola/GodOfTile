using App.Scripts.Modules.StateMachine.Services.UpdateService;
using App.Scripts.Modules.TimeProvider;
using App.Scripts.Scenes.Gameplay.Features.CameraLogic.Configs;
using App.Scripts.Scenes.Gameplay.Features.Input;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace App.Scripts.Scenes.Gameplay.Features.CameraLogic
{
    public class CameraController : ICameraController, IUpdatable
    {
        private CameraMovementConfig config;
        private Transform cameraTarget;
        private IGameInput gameInput;
        private readonly ITimeProvider timeProvider;

        private CinemachineTransposer cinemachineTransposer;

        public CameraController(
            CinemachineVirtualCamera virtualCamera,
            CameraMovementConfig config,
            Transform cameraTarget,
            IGameInput gameInput,
            ITimeProvider timeProvider
        )
        {
            this.config = config;
            this.cameraTarget = cameraTarget;
            this.gameInput = gameInput;
            this.timeProvider = timeProvider;

            cinemachineTransposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        }

        public void Update()
        {
            Move(gameInput.GetMoveVectorNormalized());
            Rotate(gameInput.GetRotationValueNormalized());
            Focus();
        }

        public void Rotate(float value)
        {
            cameraTarget.transform.Rotate(Vector3.up, value * config.RotationSpeed);
        }

        public void Move(Vector2 dir)
        {
            var targetTransform = cameraTarget.transform;
            var moveDir =
                targetTransform.forward * dir.y + targetTransform.right * dir.x;

            targetTransform.position += moveDir * timeProvider.DeltaTime * config.MoveSpeed;

            var x = Mathf.Clamp(
                targetTransform.position.x,
                config.XCoordinate.Min,
                config.XCoordinate.Max
            );
            var y = Mathf.Clamp(
                cameraTarget.transform.position.z,
                config.YCoordinate.Min,
                config.YCoordinate.Max
            );
            
            targetTransform.position = new Vector3(x, targetTransform.position.y, y);
        }

        public void Focus()
        {
            var targetFocus = cinemachineTransposer.m_FollowOffset;
            if (Mouse.current.scroll.ReadValue().y < 0)
            {
                targetFocus.y += config.FocusStep;
            }

            if (Mouse.current.scroll.ReadValue().y > 0)
            {
                targetFocus.y -= config.FocusStep;
            }

            targetFocus.y = Mathf.Clamp(targetFocus.y, config.Offset.Min, config.Offset.Max);

            cinemachineTransposer.m_FollowOffset = Vector3.Lerp(
                cinemachineTransposer.m_FollowOffset,
                targetFocus,
                timeProvider.DeltaTime * config.FocusSpeed
            );
        }
    }
}