using App.Scripts.Modules.MinMaxValue;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.CameraLogic.Configs
{
    [CreateAssetMenu(fileName = "CameraMovementConfig", menuName = "Configs/Camera/Movement")]
    public class CameraMovementConfig : ScriptableObject
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float rotationSpeed;

        [SerializeField] private float focusStep;
        [SerializeField] private float focusSpeed;

        [Header("Constraints")]
        [SerializeField] private MinMaxInt xCoordinate;

        [SerializeField] private MinMaxInt yCoordinate;

        [SerializeField] private MinMaxInt offset;

        public float MoveSpeed => moveSpeed;
        public float RotationSpeed => rotationSpeed;
        public float FocusStep => focusStep;
        public float FocusSpeed => focusSpeed;
        public MinMaxInt XCoordinate => xCoordinate;
        public MinMaxInt YCoordinate => yCoordinate;

        public MinMaxInt Offset
        {
            get => offset;
            set => offset = value;
        }
    }
}