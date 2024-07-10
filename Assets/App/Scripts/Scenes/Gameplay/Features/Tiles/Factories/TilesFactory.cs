using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles;
using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using UnityEngine;
using Zenject;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Creation.Factories
{
    public class TilesFactory : ITilesFactory
    {
        private Tile tilePrefab;
        private Transform container;
        private DiContainer diContainer;
        private TilesDatabase database;

        public TilesFactory(
            DiContainer diContainer,
            Tile tilePrefab,
            Transform container,
            TilesDatabase database
        )
        {
            this.diContainer = diContainer;
            this.tilePrefab = tilePrefab;
            this.container = container;
            this.database = database;
        }

        public Tile GetTile(string id)
        {
            return GetTile(database.Configs[id]);
        }

        public Tile GetTile(TileConfig tileConfig)
        {
            Tile tile = diContainer.InstantiatePrefabForComponent<Tile>(tilePrefab, container);
            TileConfig config = GameObject.Instantiate(tileConfig);
            tile.Initialize(config);
            return tile;
        }
    }
}
