using App.Scripts.Scenes.Gameplay.Features.Input;
using App.Scripts.Scenes.Gameplay.Features.Map.Providers.Grid;
using App.Scripts.Scenes.Gameplay.Features.Tiles.General;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Providers.Selection
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
            var gridPosition = gameInput.GetGridMousePosition();

            if (!gridProvider.IsValid(gridPosition))
            {
                return null;
            }

            var tile = gridProvider.Grid[gridPosition.x, gridPosition.y];
            return tile;
        }
    }
}