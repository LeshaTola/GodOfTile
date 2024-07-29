using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Inventory.Configs
{
    [CreateAssetMenu(fileName = "ResourcesDatabase", menuName = "Databases/Resources")]
    public class ResourcesDatabase : ScriptableObject
    {
        [field: SerializeField]
        public List<ResourceConfig> Resources { get; private set; }
    }
}