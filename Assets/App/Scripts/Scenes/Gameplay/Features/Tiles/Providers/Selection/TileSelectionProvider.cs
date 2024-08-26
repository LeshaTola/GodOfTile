using System.Threading;
using App.Scripts.Scenes.Gameplay.Features.Input;
using App.Scripts.Scenes.Gameplay.Features.Map.Providers.Grid;
using App.Scripts.Scenes.Gameplay.Features.Popups.Information.Routers;
using App.Scripts.Scenes.Gameplay.Features.Tiles.General;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.Views;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Providers.Selection
{
    public class TileSelectionProvider : ITileSelectionProvider
    {
        private IGameInput gameInput;
        private IGridProvider gridProvider;
        private IInformationPopupRouter informationPopupRouter;
        private IEffectorVisualProvider effectorVisualProvider;

        private Tile selectedTile;
        private CancellationTokenSource cts;

        public TileSelectionProvider(IGameInput gameInput,
            IGridProvider gridProvider,
            IInformationPopupRouter informationPopupRouter,
            IEffectorVisualProvider effectorVisualProvider)
        {
            this.gameInput = gameInput;
            this.gridProvider = gridProvider;
            this.informationPopupRouter = informationPopupRouter;
            this.effectorVisualProvider = effectorVisualProvider;
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

        public async void SelectTile(Tile tile)
        {
            await ShowTileInformation(tile);
        }

        private async UniTask ShowTileInformation(Tile tile)
        {
            effectorVisualProvider.Cleanup();

            if (selectedTile == null)
            {
                cts = new CancellationTokenSource();

                selectedTile = tile;
                tile.Visual.StartGlow();

                await informationPopupRouter.ShowPopup(tile.Config,cts.Token);
                Cleanup();
            }
            else
            {
                selectedTile.Visual.StopGlow();
                selectedTile = tile;
                tile.Visual.StartGlow();

                informationPopupRouter.UpdatePopup(tile.Config);

            }
            
            effectorVisualProvider.Setup(tile);
        }

        public void Cleanup()
        {
            if (selectedTile != null)
            {
                selectedTile.Visual.StopGlow();
                selectedTile = null;
            }

            effectorVisualProvider.Cleanup();
            cts?.Cancel();
        }


    }
}