using App.Scripts.Scenes.Gameplay.Features.Popups.Shop.Item;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Popups.Shop.Factories.Item
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