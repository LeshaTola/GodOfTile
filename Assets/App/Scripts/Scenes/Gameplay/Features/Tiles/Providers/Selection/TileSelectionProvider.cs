using System.Threading;
using App.Scripts.Scenes.Gameplay.Features.Input;
using App.Scripts.Scenes.Gameplay.Features.Map.Providers.Grid;
using App.Scripts.Scenes.Gameplay.Features.Popups.Information.Routers;
using App.Scripts.Scenes.Gameplay.Features.Screens.Gameplay.TileInformation.Presenters;
using App.Scripts.Scenes.Gameplay.Features.Tiles.General;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.Views;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Providers.Selection
{
    public class TileSelectionProvider : ITileSelectionProvider
    {
        private IGameInput gameInput;
        private IGridProvider gridProvider;
        private TileInformationPresenter tileInformationPresenter;
        private IEffectorVisualProvider effectorVisualProvider;

        private Tile selectedTile;
        private CancellationTokenSource cts;

        public TileSelectionProvider(IGameInput gameInput,
            IGridProvider gridProvider,
            TileInformationPresenter tileInformationPresenter,
            IEffectorVisualProvider effectorVisualProvider)
        {
            this.gameInput = gameInput;
            this.gridProvider = gridProvider;
            this.tileInformationPresenter = tileInformationPresenter;
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

            if (selectedTile == null)
            {
                effectorVisualProvider.Setup(tile);
                cts = new CancellationTokenSource();

                selectedTile = tile;
                selectedTile.Visual.StartGlow();

                tileInformationPresenter.Setup(tile.Config);
                await tileInformationPresenter.ShowUntil(cts.Token);
                
                Cleanup();
                effectorVisualProvider.Cleanup();
                return;
            }
            effectorVisualProvider.Cleanup();

            
            selectedTile.Visual.StopGlow();
            
            selectedTile = tile;
            selectedTile.Visual.StartGlow();
            effectorVisualProvider.Setup(tile);

            tileInformationPresenter.Setup(tile.Config);
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