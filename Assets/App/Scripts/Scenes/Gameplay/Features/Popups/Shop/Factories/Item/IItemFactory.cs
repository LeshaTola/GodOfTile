using App.Scripts.Scenes.Gameplay.Features.Popups.Shop.Item;

namespace App.Scripts.Scenes.Gameplay.Features.Shop.Factories.Item
{
    public interface IItemFactory
    {
        public ShopItemUI GetItem();
    }
}