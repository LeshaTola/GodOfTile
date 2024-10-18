using System.Collections.Generic;
using App.Scripts.Modules.Factories;
using App.Scripts.Modules.Localization;
using App.Scripts.Modules.Sounds;
using App.Scripts.Scenes.Gameplay.Features.Popups.InformationWidget.Presenters;
using App.Scripts.Scenes.Gameplay.Features.Popups.InformationWidget.ViewModels;
using App.Scripts.Scenes.Gameplay.Features.Popups.Shop.Item;
using App.Scripts.Scenes.Gameplay.Features.Shop.Systems;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Scenes.Gameplay.Features.Screens.Shop.Views.ShopViews.Presenter
{
    public class ShopViewPresenter
    {
        private ShopView shopView;
        private IShopSystem shopSystem;
        private ILocalizationSystem localizationSystem;
        private CostWidgetPresenter costWidgetPresenter;
        private IFactory<ShopItemView> itemViewsFactory;
        private ShopItemPresenterFactory itemsPresenterFactory;
        private IInformationWidgetViewModule informationWidgetViewModule;
        private readonly ISoundProvider soundProvider;


        private Dictionary<TileConfig, ShopItemPresenter> items = new();

        public ShopViewPresenter(
            ShopView shopView,
            IShopSystem shopSystem,
            ISoundProvider soundProvider,
            ILocalizationSystem localizationSystem,
            IFactory<ShopItemView> itemViewsFactory,
            CostWidgetPresenter costWidgetPresenter,
            ShopItemPresenterFactory itemsPresenterFactory,
            IInformationWidgetViewModule informationWidgetViewModule)
        {
            this.shopView = shopView;
            this.shopSystem = shopSystem;
            this.soundProvider = soundProvider;
            this.itemViewsFactory = itemViewsFactory;
            this.localizationSystem = localizationSystem;
            this.costWidgetPresenter = costWidgetPresenter;
            this.itemsPresenterFactory = itemsPresenterFactory;
            this.informationWidgetViewModule = informationWidgetViewModule;
        }

        public void Initialize()
        {
            shopView.Initialize(localizationSystem,informationWidgetViewModule);
            costWidgetPresenter.Initialize();
            InitializeItems();
            shopSystem.OnNewTileAdd += OnNewTileAdd;
        }

        public void Cleanup()
        {
            shopView.Cleanup();
            costWidgetPresenter.Cleanup();
            CleanupItems();

            shopSystem.OnNewTileAdd -= OnNewTileAdd;
        }

        public void Setup()
        {
            foreach (var availableTile in shopSystem.AvailableTiles)
            {
                AddTile(availableTile);
            }
        }

        public async UniTask Show()
        {
            await shopView.Show();
        }

        public async UniTask Hide()
        {
            await shopView.Hide();
        }
        
        private void OnNewTileAdd(TileConfig tileConfig)
        {
            AddTile(tileConfig);
        }

        private void AddTile(TileConfig tileConfig)
        {
            if (items.ContainsKey(tileConfig))
            {
                return;
            }
            var view = itemViewsFactory.GetItem();
            var itemPresenter = itemsPresenterFactory.GetPresenter(view, tileConfig);
            itemPresenter.Initialize(costWidgetPresenter);
            itemPresenter.Setup();
            items.Add(tileConfig, itemPresenter);
            shopView.AddItemView(view);
        }

        private void InitializeItems()
        {
            foreach (var item in items)
            {
                item.Value.Initialize(costWidgetPresenter);
            }
        }

        private void CleanupItems()
        {
            foreach (var item in items)
            {
                item.Value.Cleanup();
            }
        }
    }
}