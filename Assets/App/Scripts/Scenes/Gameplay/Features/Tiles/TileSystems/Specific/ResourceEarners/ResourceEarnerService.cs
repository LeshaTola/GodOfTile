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
        
        private List<ResourceEarner> resourceEarners = new();
        private float timer = 1f;

        public bool Active { get; set; } = true;

        public ResourceEarnerService(IInventorySystem inventorySystem, ITimeProvider timeProvider)
        {
            this.inventorySystem = inventorySystem;
            this.timeProvider = timeProvider;
        }

        public void AddResourceEarnerSystem(ResourceEarner resourceEarner)
        {
            resourceEarners.Add(resourceEarner);
        }

        public void RemoveResourceEarnerSystem(ResourceEarner resourceEarner)
        {
            resourceEarners.Remove(resourceEarner);
        }

        public void Update()
        {
            if (!Active)
            {
                return;
            }
            
            timer -= timeProvider.DeltaTime;
            if (timer <= 0)
            {
                timer = 1f;
                AddResources();
            }
        }

        private void AddResources()
        {
            foreach (var resourceEarner in resourceEarners)
            {
                var data = (ResourceEarnerSystemData) resourceEarner.Data; 
                inventorySystem.ChangeRecourseAmount(data.Resource.ResourceName, data.AmountPerSecond);
            }
        }
    }
}