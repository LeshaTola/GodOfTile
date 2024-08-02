using System.Collections.Generic;
using App.Scripts.Modules.StateMachine.Services.UpdateService;
using App.Scripts.Modules.TimeProvider;
using App.Scripts.Scenes.Gameplay.Features.Inventory.Systems;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.ResourceEarners
{
    public class ResourceEarnerService : IUpdatable, IResourceEarnerService
    {
        private IInventorySystem inventorySystem;
        private  ITimeProvider timeProvider;

        private Dictionary<string, float> resourceExtraction = new();
        private float timer = 1f;

        public ResourceEarnerService(IInventorySystem inventorySystem, ITimeProvider timeProvider)
        {
            this.inventorySystem = inventorySystem;
            this.timeProvider = timeProvider;
        }

        public void AddResourceEarnerSystem(string resourceName, float amountPerSecond)
        {
            if (!resourceExtraction.ContainsKey(resourceName))
            {
                resourceExtraction.Add(resourceName, amountPerSecond);
                return;
            }

            resourceExtraction[resourceName] += amountPerSecond;
        }

        public void RemoveResourceEarnerSystem(string resourceName, float amountPerSecond)
        {
            if (!resourceExtraction.ContainsKey(resourceName))
            {
                return;
            }

            resourceExtraction[resourceName] -= amountPerSecond;
        }

        public void Update()
        {
            timer -= timeProvider.DeltaTime;
            if (timer <= 0)
            {
                timer = 1f;
                AddResources();
            }
        }

        private void AddResources()
        {
            foreach (var resource in resourceExtraction)
            {
                inventorySystem.ChangeRecourseAmount(resource.Key, resource.Value);
            }
        }
    }
}