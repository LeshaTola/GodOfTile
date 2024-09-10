using System.Linq;
using System.Threading;
using App.Scripts.Modules.Localization;
using App.Scripts.Modules.StateMachine.Services.CleanupService;
using App.Scripts.Modules.StateMachine.Services.InitializeService;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Factories.TileSystemUIProvider;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Screens.Gameplay.TileInformation.Presenters
{
    public class TileInformationPresenter: IInitializable, ICleanupable
    {
        private TileInformationView view;
        private ILocalizationSystem localizationSystem;
        private ITileSystemUIProvidersFactory tileSystemUIProvidersFactory;
        
        private CancellationTokenSource cts;

        public TileInformationPresenter(
            TileInformationView view,
            ILocalizationSystem localizationSystem,
            ITileSystemUIProvidersFactory tileSystemUIProvidersFactory)
        {
            this.view = view;
            this.localizationSystem = localizationSystem;
            this.tileSystemUIProvidersFactory = tileSystemUIProvidersFactory;
        }

        public void Initialize()
        {
            view.Initialize(localizationSystem);
            view.OnCloseButtonClicked += Cancel;
        }

        public void Cleanup()
        {
            view.Cleanup();
            view.OnCloseButtonClicked -= Cancel;
        }
        
        public void Setup(TileConfig tileConfig)
        {
            var systemUIs 
                = tileConfig.ActiveSystems.Select(
                        system => 
                            tileSystemUIProvidersFactory
                                .GetSystemUIProvider(system.Data.SystemUIProvider)
                                .GetSystemUI(system)
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
        
        private async UniTask WaitForButtonPress(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                await UniTask.Yield();
            }
        }
    }
}