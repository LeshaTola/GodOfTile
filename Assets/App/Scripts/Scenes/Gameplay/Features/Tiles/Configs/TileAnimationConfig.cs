using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Configs
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

        public float ActiveAnimationDuration => activeAnimationDuration;
        public float CreateAnimationDuration => createAnimationDuration;

        public float DestroyAnimationDuration => destroyAnimationDuration;
        public float RotationAnimationDuration => rotationAnimationDuration;
    }
}