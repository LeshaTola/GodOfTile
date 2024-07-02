using UnityEngine;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Configs
{
	[CreateAssetMenu(fileName = "TileAnimationConfig", menuName = "Configs/Tiles/Animation")]
	public class TileAnimationConfig : ScriptableObject
	{
		[SerializeField]
		private float activeAnimationDuration = 0.3f;

		[SerializeField]
		private float rotationAnimationDuration = 0.3f;

		[SerializeField]
		private float createAnimationDuration = 0.3f;

		[SerializeField]
		private float destroyAnimationDuration = 0.3f;

		public float ActiveAnimationDuration
		{
			get => activeAnimationDuration;
		}
		public float CreateAnimationDuration
		{
			get => createAnimationDuration;
		}
		public float DestroyAnimationDuration
		{
			get => destroyAnimationDuration;
		}
		public float RotationAnimationDuration
		{
			get => rotationAnimationDuration;
		}
	}
}
