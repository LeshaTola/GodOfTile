using App.Scripts.Scenes.Gameplay.Features.Popups.InformationWidget;
using App.Scripts.Scenes.Gameplay.Features.Popups.InformationWidget.Cost;
using App.Scripts.Scenes.Gameplay.Features.Popups.InformationWidget.Presenters;
using App.Scripts.Scenes.Gameplay.Features.Popups.Shop.Item;
using App.Scripts.Scenes.Gameplay.Features.Screens.Shop;
using App.Scripts.Scenes.Gameplay.Features.Screens.Shop.Presenters;
using App.Scripts.Scenes.Gameplay.Features.Screens.Shop.Views.ShopViews;
using App.Scripts.Scenes.Gameplay.Features.Screens.Shop.Views.ShopViews.Presenter;
using App.Scripts.Scenes.Gameplay.Features.Shop.Systems;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Services.PlacementCost;
using UnityEngine;
using Zenject;

namespace App.Scripts.Scenes.Gameplay.Bootstrap.Shop
{
    public class ShopInstaller : MonoInstaller
    {
        [SerializeField] private ShopItemView shopItemViewTemplate;
        [SerializeField] private CostUI costUITemplate;
        [SerializeField] private ShopView shopView;
        [SerializeField] private ShopScreen shopScreen;
        [SerializeField] private CostWidget costWidget;

        public override void InstallBindings()
        {
            Container.BindInterfacesTo<ShopSystem>().AsSingle();
            Container.Bind<ShopItemPresenterFactory>().AsSingle();

            Container.Bind<ShopScreenPresenter>().AsSingle();
            Container.Bind<ShopScreen>().FromInstance(shopScreen).AsSingle();
            
            Container.Bind<ShopViewPresenter>().AsSingle();
            Container.Bind<ShopView>().FromInstance(shopView).AsSingle();
            
            Container.Bind<CostWidgetPresenter>().AsSingle().WithArguments(costWidget);

            Container.Bind<Modules.Factories.IFactory<ShopItemView>>()
                .To<Modules.Factories.MonoFactories.MonoFactory<ShopItemView>>()
                .AsSingle().WithArguments(shopItemViewTemplate);

            Container.Bind<Modules.Factories.IFactory<CostUI>>()
                .To<Modules.Factories.MonoFactories.MonoFactory<CostUI>>()
                .AsSingle().WithArguments(costUITemplate);
            
        }
    }
}