using App.Scripts.Scenes.Gameplay.Features.Popups.InformationWidget.Cost;
using App.Scripts.Scenes.Gameplay.Features.Popups.Shop.Item;
using App.Scripts.Scenes.Gameplay.Features.Shop.Factories.Cost;
using App.Scripts.Scenes.Gameplay.Features.Shop.Factories.Item;
using App.Scripts.Scenes.Gameplay.Features.Shop.Systems;
using UnityEngine;
using Zenject;

namespace App.Scripts.Scenes.Gameplay.Bootstrap.Shop
{
    public class ShopInstaller : MonoInstaller
    {
        [SerializeField]
        private ShopItemUI shopItemUITemplate;

        [SerializeField]
        private CostUI costUITemplate;

        public override void InstallBindings()
        {
            Container
                .Bind<IItemFactory>()
                .To<ItemFactory>()
                .AsSingle()
                .WithArguments(shopItemUITemplate);
            Container
                .Bind<ICostUIFactory>()
                .To<CostUIFactory>()
                .AsSingle()
                .WithArguments(costUITemplate);

            Container.BindInterfacesTo<ShopSystem>().AsSingle();
        }
    }
}