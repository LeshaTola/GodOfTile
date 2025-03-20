using System;
using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.Inventory.Configs;
using App.Scripts.Scenes.Gameplay.Features.Inventory.DTO;

namespace App.Scripts.Scenes.Gameplay.Features.Inventory.Systems
{
    public class InventorySystem : IInventorySystem
    {
        private ResourcesDatabase resourcesDatabase;

        public InventorySystem(ResourcesDatabase resourcesDatabase)
        {
            this.resourcesDatabase = resourcesDatabase;
        }

        public Dictionary<string, float> Resources { get; private set; }

        public event Action<string, float> OnRecourseAmountChanged;

        public void ChangeRecourseAmount(string resourceName, float amount)
        {
            if (!Resources.ContainsKey(resourceName))
            {
                return;
            }

            Resources[resourceName] += amount;

            OnRecourseAmountChanged?.Invoke(resourceName, Resources[resourceName]);
        }

        public bool IsEnough(List<ResourceCount> resourcesCounts)
        {
            foreach (var resourceCount in resourcesCounts)
            {
                if (IsEnough(resourceCount))
                {
                    continue;
                }

                return false;
            }

            return true;
        }

        public bool IsEnough(ResourceCount resourceCount)
        {
            return IsEnough(resourceCount.Resource.ResourceName, resourceCount.Count);
        }

        public InventoryState GetState()
        {
            return new()
            {
                Resources = Resources
            };
        }

        public void SetState(InventoryState state)
        {
            foreach (var resource in state.Resources)
            {
                Resources[resource.Key] = resource.Value;
            }
        }

        public bool IsEnough(string resourceName, float amount)
        {
            if (!Resources.ContainsKey(resourceName))
            {
                return false;
            }

            return Resources[resourceName] >= amount;
        }

        public void InitializeResources()
        {
            Resources = new Dictionary<string, float>(resourcesDatabase.Resources.Count);

            foreach (var resource in resourcesDatabase.Resources)
            {
                Resources.Add(resource.ResourceName, resource.StartAmount);
                OnRecourseAmountChanged?.Invoke(resource.ResourceName, resource.StartAmount);
            }
        }
    }

    public class InventoryState
    {
        public Dictionary<string, float> Resources;
    }
}