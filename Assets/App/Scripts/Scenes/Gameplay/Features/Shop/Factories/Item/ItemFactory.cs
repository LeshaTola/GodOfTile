using Assets.App.Scripts.Scenes.Gameplay.Features.Shop.UI.Item;
using UnityEngine;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Shop.Factories.Item
{
    public class ItemFactory : IItemFactory
    {
        private ShopItemUI itemTemplate;

        public ItemFactory(ShopItemUI itemTemplate)
        {
            this.itemTemplate = itemTemplate;
        }

        public ShopItemUI GetItem()
        {
            return GameObject.Instantiate(itemTemplate);
        }
    }
}
