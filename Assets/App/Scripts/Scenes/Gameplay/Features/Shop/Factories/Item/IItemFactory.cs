using App.Scripts.Scenes.Gameplay.Features.Popups.ShopPopup.Item;

namespace App.Scripts.Scenes.Gameplay.Features.Shop.Factories.Item
{
    public interface IItemFactory
    {
        public ShopItemUI GetItem();
    }
}