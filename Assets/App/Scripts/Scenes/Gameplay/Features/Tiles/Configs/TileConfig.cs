using System.Collections.Generic;
using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Configs.Plate;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Configs
{
    [CreateAssetMenu(fileName = "TileConfig", menuName = "Configs/Tiles/Tile")]
    public class TileConfig : ScriptableObject
    {
        [SerializeField]
        private Vector2Int size = Vector2Int.one;

        [SerializeField]
        private TileTypeDatabase tileTypeDatabase;

        [SerializeField]
        [ValueDropdown(nameof(GetTileTypes))]
        [ShowIf("@tileTypeDatabase !=null")]
        private Material type;

        public Vector2Int Size
        {
            get => size;
        }
        public Material Type
        {
            get => type;
        }

        private IEnumerable<ValueDropdownItem<Material>> GetTileTypes()
        {
            if (tileTypeDatabase == null)
            {
                return null;
            }

            var materialList = new List<ValueDropdownItem<Material>>();
            foreach (var key in tileTypeDatabase.TileTypesDatabase.Keys)
            {
                materialList.Add(
                    new ValueDropdownItem<Material>(key, tileTypeDatabase.TileTypesDatabase[key])
                );
            }

            return materialList;
        }
    }
}
