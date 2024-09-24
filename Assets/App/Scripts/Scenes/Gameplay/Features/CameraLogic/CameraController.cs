using App.Scripts.Modules.StateMachine.Services.UpdateService;
using App.Scripts.Modules.TimeProvider;
using App.Scripts.Scenes.Gameplay.Features.CameraLogic.Configs;
using App.Scripts.Scenes.Gameplay.Features.Input;
using Cinemachine;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.CameraLogic
{
    public class CameraController : ICameraController, IUpdatable
    {
        private CameraMovementConfig config;
        private Transform cameraTarget;
        private IGameInput gameInput;
        private readonly ITimeProvider timeProvider;

        private CinemachineTransposer cinemachineTransposer;

        public bool Active { get; set; } = true;

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
            if (!Active)
            {
                return;
            }

            Move(gameInput.GetMoveVectorNormalized());
            Rotate(gameInput.GetRotationValueNormalized());
            Focus();
        }

        public void Rotate(float value)
        {
            cameraTarget.transform.Rotate(Vector3.up, value *timeProvider.DeltaTime * config.RotationSpeed);
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

            targetFocus.y += config.FocusStep * gameInput.GetMouseScrollNormalized();
            targetFocus.y = Mathf.Clamp(targetFocus.y, config.Offset.Min, config.Offset.Max);

            cinemachineTransposer.m_FollowOffset = Vector3.Lerp(
                cinemachineTransposer.m_FollowOffset,
                targetFocus,
                timeProvider.DeltaTime * config.FocusSpeed
            );
        }
    }
}