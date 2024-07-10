using Assets.App.Scripts.Scenes.Gameplay.Features.Shop.Factories.Cost;
using Assets.App.Scripts.Scenes.Gameplay.Features.Shop.Factories.Item;
using Assets.App.Scripts.Scenes.Gameplay.Features.Shop.Systems;
using Assets.App.Scripts.Scenes.Gameplay.Features.Shop.UI.Cost;
using Assets.App.Scripts.Scenes.Gameplay.Features.Shop.UI.Item;
using UnityEngine;
using Zenject;

namespace Assets.App.Scripts.Scenes.Gameplay.Bootstrap.Shop
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

            Container.Bind<IShopSystem>().To<ShopSystem>().AsSingle();
        }
    }
}
