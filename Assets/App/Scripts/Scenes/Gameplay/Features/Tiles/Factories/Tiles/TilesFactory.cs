using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Factories;
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

        public Tile GetTile(string id)
        {
            return GetTile(database.Configs[id]);
        }

        public Tile GetTile(TileConfig tileConfig)
        {
            var tile = diContainer.InstantiatePrefabForComponent<Tile>(tilePrefab, container);
            var config = Object.Instantiate(tileConfig);
            var systems = systemsFactory.GetSystems(config.Systems);
            tile.Initialize(config,systems);
            return tile;
        }
    }
}