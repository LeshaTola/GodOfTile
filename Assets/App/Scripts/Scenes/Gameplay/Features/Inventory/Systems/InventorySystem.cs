using System;
using System.Collections.Generic;
using Assets.App.Scripts.Scenes.Gameplay.Features.Inventory.Configs;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Inventory.Systems
{
    public class InventorySystem : IInventorySystem
    {
        private ResourcesDatabase resourcesDatabase;

        public InventorySystem(ResourcesDatabase resourcesDatabase)
        {
            this.resourcesDatabase = resourcesDatabase;
        }

        public Dictionary<string, int> Resources { get; private set; }

        public event Action<string, int> OnRecourseAmountChanged;

        public void ChangeRecourseAmount(string resourceName, int amount)
        {
            if (!Resources.ContainsKey(resourceName))
            {
                return;
            }

            Resources[resourceName] += amount;

            OnRecourseAmountChanged?.Invoke(resourceName, Resources[resourceName]);
        }

        public bool IsEnough(string resourceName, int amount)
        {
            if (!Resources.ContainsKey(resourceName))
            {
                return false;
            }

            return Resources[resourceName] >= amount;
        }

        public void InitializeResources()
        {
            Resources = new(resourcesDatabase.Resources.Count);

            foreach (var resource in resourcesDatabase.Resources)
            {
                Resources.Add(resource.ResourceName, resource.StartAmount);
                OnRecourseAmountChanged?.Invoke(resource.ResourceName, resource.StartAmount);
            }
        }
    }
}
