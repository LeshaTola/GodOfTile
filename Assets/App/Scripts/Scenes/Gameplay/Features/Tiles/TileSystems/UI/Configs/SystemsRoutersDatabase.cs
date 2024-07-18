using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI.Specific.ResourceEarner.Routers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI.Configs
{
    [CreateAssetMenu(fileName = "SystemsRoutersDatabase", menuName = "Databases/Tile/Systems/Routers")]
    public class SystemsRoutersDatabase : SerializedScriptableObject
    {
        [SerializeField] private Dictionary<TileSystem, ISystemRouter> routers;

        public Dictionary<TileSystem, ISystemRouter> Routers => routers;
    }
}