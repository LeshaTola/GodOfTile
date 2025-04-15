using System.Collections.Generic;
using System.IO;
using App.Scripts.Scenes.Gameplay.Features.Inventory.DTO;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs.Plate;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Scenes.Gameplay.Features.LevelCreation.Editor
{
    public class TileConfigGeneratorWindow : OdinEditorWindow
    {
        [MenuItem("Tools/Tile Config Generator")]
        private static void OpenWindow()
        {
            GetWindow<TileConfigGeneratorWindow>().Show();
        }

        [Title("Configuration")]
        [FolderPath]
        [SerializeField]
        private string outputFolder = "Assets/Configs/Tiles";

        [SerializeField]
        private TileTypeDatabase tileTypeDatabase;

        [SerializeField]
        private Vector2Int defaultSize = Vector2Int.one;

        [SerializeField]
        private Sprite defaultTileImage;

        [SerializeField]
        private string defaultTileNamePostfix = "TileConfig";

        [SerializeField]
        private string defaultDescription = "A generated tile";

        [Title("GameObjects to Process")]
        [ListDrawerSettings(DraggableItems = false, Expanded = true)]
        [SerializeField]
        private List<GameObject> gameObjects = new List<GameObject>();

        [Button(ButtonSizes.Large)]
        [PropertyOrder(-1)]
        private void GenerateTileConfigs()
        {
            if (gameObjects.Count == 0)
            {
                Debug.LogWarning("No GameObjects to process");
                return;
            }

            if (tileTypeDatabase == null)
            {
                Debug.LogError("TileTypeDatabase is not assigned");
                return;
            }

            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
                AssetDatabase.Refresh();
            }

            foreach (var gameObject in gameObjects)
            {
                if (gameObject == null) continue;

                CreateTileConfig(gameObject);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private void CreateTileConfig(GameObject gameObject)
        {
            var config = ScriptableObject.CreateInstance<TileConfig>();

            config.name = $"{gameObject.name}{defaultTileNamePostfix}";

            string type = string.Empty;
            var types = new List<string>(tileTypeDatabase.Types.Keys);
            if (types.Count > 0)
            {
                type = types[0];
            }
            
            config.Setup(tileTypeDatabase, gameObject,type,defaultTileImage,gameObject.name,defaultDescription,new(),new());
            
            string assetPath = Path.Combine(outputFolder, $"{config.name}.asset");
            AssetDatabase.CreateAsset(config, assetPath);
            Debug.Log($"Created TileConfig at: {assetPath}");
        }
    }
}