using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using Zenject;

namespace App.Scripts.Scenes.Gameplay.Features.Screens.Shop.Views.Item
{
    public class ShopItemPresenterFactory
    {
        private DiContainer diContainer;

        public ShopItemPresenterFactory(DiContainer diContainer)
        {
            this.diContainer = diContainer;
        }

        public ShopItemPresenter GetPresenter(ShopItemView shopItemView, TileConfig tileConfig)
        {
            return diContainer.Instantiate<ShopItemPresenter>(new object[]
            {
                shopItemView,
                tileConfig
            });
        }
    }
}