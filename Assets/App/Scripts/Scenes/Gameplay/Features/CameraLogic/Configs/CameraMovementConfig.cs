using Module.MinMaxValue;
using UnityEngine;


namespace Assets.App.Scripts.Scenes.Gameplay.Features.CameraLogic.Configs
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

		public float MoveSpeed { get => moveSpeed; }
		public float RotationSpeed { get => rotationSpeed; }
		public float FocusStep { get => focusStep; }
		public float FocusSpeed { get => focusSpeed; }
		public MinMaxInt XCoordinate { get => xCoordinate; }
		public MinMaxInt YCoordinate { get => yCoordinate; }
		public MinMaxInt Offset { get => offset; set => offset = value; }
	}

}