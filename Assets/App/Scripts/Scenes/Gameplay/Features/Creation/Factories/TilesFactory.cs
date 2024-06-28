using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles;
using UnityEngine;
using Zenject;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Creation.Factories
{
    public class TilesFactory : ITilesFactory
    {
        private Tile tilePrefab;
        private Transform container;
        private DiContainer diContainer;

        public TilesFactory(DiContainer diContainer, Tile tilePrefab, Transform container)
        {
            this.diContainer = diContainer;
            this.tilePrefab = tilePrefab;
            this.container = container;
        }

        public Tile GetTile(string id)
        {
            Tile tile = diContainer.InstantiatePrefabForComponent<Tile>(tilePrefab, container);
            tile.Initialize();
            return tile;
        }
    }
}
