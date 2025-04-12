using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Factories.TileSystem;
using App.Scripts.Scenes.Gameplay.Features.Tiles.General;
using UnityEngine;
using Zenject;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Factories.Tiles
{
    public class TilesFactory : ITilesFactory
    {
        private Tile tilePrefab;
        private Transform container;
        private ISystemsFactory systemsFactory;
        private DiContainer diContainer;
        private TilesDatabase database;

        public TilesFactory(
            DiContainer diContainer,
            Tile tilePrefab,
            Transform container,
            TilesDatabase database,
            ISystemsFactory systemsFactory
        )
        {
            this.diContainer = diContainer;
            this.tilePrefab = tilePrefab;
            this.container = container;
            this.database = database;
            this.systemsFactory = systemsFactory;
        }

        public Tile GetTile(string id, bool isActive = true)
        {
            return GetTile(database.Configs[id],isActive);
        }

        public Tile GetTile(TileConfig tileConfig, bool isActive = true)
        {
            var tile = diContainer.InstantiatePrefabForComponent<Tile>(tilePrefab, container);
            var config = Object.Instantiate(tileConfig);
            if (isActive)
            {
                var systems = systemsFactory.GetSystems(config.Systems, tile);
                config.ActiveSystems = systems;
            }
            tile.Initialize(config);
            return tile;
        }
    }
}