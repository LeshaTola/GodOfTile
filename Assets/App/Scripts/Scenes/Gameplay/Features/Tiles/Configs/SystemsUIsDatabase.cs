using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Configs
{
    [CreateAssetMenu (fileName = "SystemsUIsDatabase", menuName = "Databases/Tiles/SystemUIs")]
    public class SystemsUIsDatabase : ScriptableObject
    {
        [field: SerializeField] public List<SystemUI> UIs { get; private set; }
    }
}