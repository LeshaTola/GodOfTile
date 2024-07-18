using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI.Configs
{
    [CreateAssetMenu(fileName = "SystemsUIDatabase", menuName = "Databases/Tile/Systems/UI")]
    public class SystemsUIDatabase : SerializedScriptableObject
    {
        [SerializeField] private Dictionary<TileSystem, SystemUI> systemsUIs;

        public Dictionary<TileSystem, SystemUI> SystemsUIs => systemsUIs;
    }
}