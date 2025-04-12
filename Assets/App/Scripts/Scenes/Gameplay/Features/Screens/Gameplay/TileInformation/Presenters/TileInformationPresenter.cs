using System.Linq;
using System.Threading;
using App.Scripts.Modules.Localization;
using App.Scripts.Modules.StateMachine.Services.CleanupService;
using App.Scripts.Modules.StateMachine.Services.InitializeService;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Services.TilesCreation;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Factories.TileSystemUIProvider;
using App.Scripts.Scenes.Gameplay.Features.Tiles.General;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Screens.Gameplay.TileInformation.Presenters
{
    public class TileInformationPresenter: IInitializable, ICleanupable
    {
        private readonly TileInformationView view;
        private readonly ILocalizationSystem localizationSystem;
        private readonly ITileSystemUIProvidersFactory tileSystemUIProvidersFactory;
        private readonly ITilesCreationService tilesCreationService;
        
        private CancellationTokenSource cts;
        private Tile tile;

        public TileInformationPresenter(TileInformationView view,
            ILocalizationSystem localizationSystem,
            ITileSystemUIProvidersFactory tileSystemUIProvidersFactory,
            ITilesCreationService tilesCreationService)
        {
            this.view = view;
            this.localizationSystem = localizationSystem;
            this.tileSystemUIProvidersFactory = tileSystemUIProvidersFactory;
            this.tilesCreationService = tilesCreationService;
        }

        public void Initialize()
        {
            view.Initialize(localizationSystem);
            
            view.OnCloseButtonClicked += Cancel;
            view.OnDeleteButtonClicked += DestroyTile;
        }

        public void Cleanup()
        {
            view.Cleanup();
            
            view.OnCloseButtonClicked -= Cancel;
            view.OnDeleteButtonClicked -= DestroyTile;
        }
        
        public void Setup(Tile tile)
        {
            this.tile = tile;
            var tileConfig = this.tile.Config;
            var systemUIs 
                = tileConfig.ActiveSystems.Select(
                        system => 
                            tileSystemUIProvidersFactory
                                .GetSystemUIProvider(system.Data.SystemUIProvider)
                                ?.GetSystemUI(system)
                            ).ToList() ;

            view.CleanupSystems();
            view.Setup(tileConfig, systemUIs);
        }

        public async UniTask ShowUntil(CancellationToken cancellationToken)
        {
            cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            await view.Show();
            await WaitForButtonPress(cts.Token);
            await view.Hide();
        }
        
        public async UniTask Show()
        {
            await view.Show();
        }
        
        public async UniTask Hide()
        {
            Cancel();
            await view.Hide();
        }

        public void Cancel()
        {
            if (cts == null)
            {
                return;
            }
            
            cts.Cancel();
        }

        private void DestroyTile()
        {
            tilesCreationService.DestroyTile(tile.Position);
            Cancel();
        }
        
        private async UniTask WaitForButtonPress(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                await UniTask.Yield();
            }
        }
    }
}