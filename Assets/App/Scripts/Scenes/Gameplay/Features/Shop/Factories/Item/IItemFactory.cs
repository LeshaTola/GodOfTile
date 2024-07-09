using Assets.App.Scripts.Scenes.Gameplay.Features.Shop.UI.Item;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Shop.Factories.Item
{
    public interface IItemFactory
    {
        public ShopItemUI GetItem();
    }
}
