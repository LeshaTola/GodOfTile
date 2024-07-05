using Cinemachine;
using Features.StateMachineCore;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.CameraLogic.Configs
{
    public class CameraController : ICameraController, IUpdatable
    {
        private CameraMovementConfig config;
        private Transform cameraTarget;
        private IGameInput gameInput;

        private CinemachineTransposer cinemachineTransposer;

        public CameraController(
            CinemachineVirtualCamera virtualCamera,
            CameraMovementConfig config,
            Transform cameraTarget,
            IGameInput gameInput
        )
        {
            this.config = config;
            this.cameraTarget = cameraTarget;
            this.gameInput = gameInput;

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
            Debug.Log(value);
            cameraTarget.transform.Rotate(Vector3.up, value * config.RotationSpeed);
        }

        public void Move(Vector2 dir)
        {
            var moveDir =
                cameraTarget.transform.forward * dir.y + cameraTarget.transform.right * dir.x;

            cameraTarget.transform.position += moveDir * Time.deltaTime * config.MoveSpeed;

            var x = Mathf.Clamp(
                cameraTarget.transform.position.x,
                config.XCoordinate.Min,
                config.XCoordinate.Max
            );
            var y = Mathf.Clamp(
                cameraTarget.transform.position.z,
                config.YCoordinate.Min,
                config.YCoordinate.Max
            );

            cameraTarget.transform.position = new Vector3(x, cameraTarget.transform.position.y, y);
        }

        public void Focus()
        {
            Vector3 targetFocus = cinemachineTransposer.m_FollowOffset;
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
                Time.deltaTime * config.FocusSpeed
            );
        }
    }
}
