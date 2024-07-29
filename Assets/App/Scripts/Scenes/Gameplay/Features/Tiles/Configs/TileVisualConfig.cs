using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Configs
{
    [CreateAssetMenu(fileName = "TileVisualConfig", menuName = "Configs/Tiles/Visual")]
    public class TileVisualConfig : ScriptableObject
    {
        [Header("Materials")]
        [SerializeField]
        private Material wrongMaterial;

        [SerializeField]
        private Material correctMaterial;

        public Material WrongMaterial => wrongMaterial;

        public Material CorrectMaterial => correctMaterial;
    }
}