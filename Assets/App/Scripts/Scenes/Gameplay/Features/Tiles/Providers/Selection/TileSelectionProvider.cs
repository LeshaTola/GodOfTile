using Assets.App.Scripts.Scenes.Gameplay.Features.Grid;
using UnityEngine;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Providers
{
    public class TileSelectionProvider : ITileSelectionProvider
    {
        private IGameInput gameInput;
        private IGridProvider gridProvider;

        public TileSelectionProvider(IGameInput gameInput, IGridProvider gridProvider)
        {
            this.gameInput = gameInput;
            this.gridProvider = gridProvider;
        }

        public Tile GetTileAtMousePosition()
        {
            Vector2Int gridPosition = gameInput.GetGridMousePosition();

            if (!gridProvider.IsValid(gridPosition))
            {
                return null;
            }

            Tile tile = gridProvider.Grid[gridPosition.x, gridPosition.y];
            return tile;
        }
    }
}
