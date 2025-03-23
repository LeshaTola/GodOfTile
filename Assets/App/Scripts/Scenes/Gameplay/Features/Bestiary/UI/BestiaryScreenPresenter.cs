using System.Linq;
using App.Scripts.Modules.Localization;
using App.Scripts.Modules.ObjectPool.Pools;
using App.Scripts.Modules.StateMachine.Services.CleanupService;
using App.Scripts.Modules.StateMachine.Services.InitializeService;
using App.Scripts.Scenes.Gameplay.Features.CraftSystem.Configs;
using App.Scripts.Scenes.Gameplay.Features.CraftSystem.Providers;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Factories.TileSystemUIProvider;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Providers.Collection;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Scenes.Gameplay.Features.Bestiary.UI
{
    public class BestiaryScreenPresenter : IInitializable, ICleanupable
    {
        private TilesDatabase tilesDatabase;
        private readonly BestiaryScreen bestiaryScreen;
        private readonly IRecipeProvider recipeProvider;
        private readonly ILocalizationSystem localizationSystem;
        private readonly IPool<BestiaryElement> bestiaryElementPool;
        private readonly ITileCollectionProvider tileCollectionProvider;
        private readonly ITileSystemUIProvidersFactory tileSystemUIProvidersFactory;

        private BestiaryElement selectedBestiaryElement;

        public BestiaryScreenPresenter(BestiaryScreen bestiaryScreen,
            IRecipeProvider recipeProvider,
            ILocalizationSystem localizationSystem,
            ITileCollectionProvider tileCollectionProvider,
            ITileSystemUIProvidersFactory tileSystemUIProvidersFactory,
            IPool<BestiaryElement> bestiaryElementPool)
        {
            this.bestiaryScreen = bestiaryScreen;
            this.tileCollectionProvider = tileCollectionProvider;
            this.recipeProvider = recipeProvider;
            this.localizationSystem = localizationSystem;
            this.tileSystemUIProvidersFactory = tileSystemUIProvidersFactory;
            this.bestiaryElementPool = bestiaryElementPool;
        }

        public void Initialize()
        {
            tileCollectionProvider.OnNewTileAdd += OnNewTileAdd;
            
            bestiaryScreen.Initialize(localizationSystem);
            bestiaryScreen.OnCloseButtonClicked += Close;
            SetupElements();
        }

        public void Cleanup()
        {
            tileCollectionProvider.OnNewTileAdd -= OnNewTileAdd;
            
            bestiaryScreen.OnCloseButtonClicked -= Close;
            bestiaryScreen.Cleanup();
        }

        public async UniTask Show()
        {
            await bestiaryScreen.Show();
        }

        public async UniTask Hide()
        {
            await bestiaryScreen.Hide();
        }

        private void SetupElements()
        {
            CleanupElements();
            foreach (var tileConfig in tileCollectionProvider.Collection)
            {
                var element = bestiaryElementPool.Get();
                element.Setup(tileConfig);
                element.OnElementClicked += OnElementClicked;
            }

            SetupScreen(tileCollectionProvider.Collection.First(), null);
        }

        private void CleanupElements()
        {
            foreach (var element in bestiaryElementPool.Active.ToList())
            {
                element.Cleanup();
                bestiaryElementPool.Release(element);
            }
        }

        private void SetupScreen(TileConfig tileConfig, RecipeSO recipeSo)
        {
            var systemUIs 
                = tileConfig.Systems.Select(
                    system => 
                        tileSystemUIProvidersFactory
                            .GetSystemUIProvider(system.Data.SystemUIProvider)
                            ?.GetSystemUI(system)
                ).ToList();
            
            bestiaryScreen.SetupTileInformation(tileConfig, systemUIs);
            bestiaryScreen.SetupRecipe(recipeSo);
        }

        private void OnElementClicked(TileConfig tileConfig, BestiaryElement bestiaryElement)
        {
            SelectElement(bestiaryElement);
            var recipe = recipeProvider.GetRecipe(tileConfig);
            SetupScreen(tileConfig, recipe);
        }

        private void SelectElement(BestiaryElement bestiaryElement)
        {
            if (selectedBestiaryElement != null)
            {
                selectedBestiaryElement.SetSelected(false);
            }

            selectedBestiaryElement = bestiaryElement;
            selectedBestiaryElement.SetSelected(true);
        }

        private void OnNewTileAdd(TileConfig obj)
        {
            SetupElements();
        }
        
        private void Close()
        {
            Hide().Forget();
        }
    }
}