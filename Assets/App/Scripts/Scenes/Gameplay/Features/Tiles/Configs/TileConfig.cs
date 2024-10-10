using System.Collections.Generic;
using System.Linq;
using App.Scripts.Scenes.Gameplay.Features.Inventory.DTO;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs.Plate;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Configs
{
    [CreateAssetMenu(fileName = "TileConfig", menuName = "Configs/Tiles/Tile")]
    public class TileConfig : SerializedScriptableObject
    {
        [SerializeField] private string id;
        [SerializeField] private Vector2Int size = Vector2Int.one;
        [SerializeField] private TileTypeDatabase tileTypeDatabase;
        [SerializeField] private GameObject building;

        [SerializeField]
        [ValueDropdown(nameof(GetTileTypes))]
        [ShowIf("@tileTypeDatabase !=null")]
        private string type;

        [PreviewField]
        [SerializeField]
        [FoldoutGroup("Extra Information")]
        private Sprite tileImage;

        [SerializeField]
        [FoldoutGroup("Extra Information")]
        private string tileName;

        [TextArea]
        [SerializeField]
        [FoldoutGroup("Extra Information")]
        private string description;

        [SerializeField] private List<ResourceCount> cost;
        [SerializeField] private List<TileSystem> systems;

        private List<TileSystem> activeSystems;

        public Vector2Int Size => size;
        public string Type => type;
        public Material TypeMaterial => tileTypeDatabase.Types[type];
        public GameObject Building => building;
        public Sprite TileSprite => tileImage;
        public string Description => description;
        public string Name => tileName;
        public string Id => id;
        public List<ResourceCount> Cost => cost;
        public List<TileSystem> Systems => systems;

        public List<TileSystem> ActiveSystems
        {
            get => activeSystems;
            set => activeSystems = value;
        }

        private void OnValidate()
        {
            id = name;
        }

        private IEnumerable<string> GetTileTypes()
        {
            if (tileTypeDatabase == null)
            {
                return null;
            }

            return new List<string>(tileTypeDatabase.Types.Keys);
        }

        public T GetSystem<T>() where T: TileSystem
        {
            var type = typeof(T);
            return (T)activeSystems.FirstOrDefault(x => x.GetType().Equals(type));
        }
    }
}