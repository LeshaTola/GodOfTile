using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Configs.Plate
{
    [CreateAssetMenu(fileName = "TileTypeDatabase", menuName = "Databases/Tiles/Types")]
    public class TileTypeDatabase : SerializedScriptableObject
    {
        [SerializeField]
        private Dictionary<string, Material> types;

        public Dictionary<string, Material> Types => types;
    }
}