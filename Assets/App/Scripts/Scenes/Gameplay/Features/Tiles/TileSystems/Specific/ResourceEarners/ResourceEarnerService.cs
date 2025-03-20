using System;
using System.Collections.Generic;
using App.Scripts.Modules.StateMachine.Services.UpdateService;
using App.Scripts.Modules.TimeProvider;
using App.Scripts.Scenes.Gameplay.Features.Inventory.DTO;
using App.Scripts.Scenes.Gameplay.Features.Inventory.Systems;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.ResourceEarners
{
    public class ResourceEarnerService : IUpdatable, IResourceEarnerService
    {
        public event Action<List<ResourceCount>> OnResourceEarned;
        
        private IInventorySystem inventorySystem;
        private ITimeProvider timeProvider;

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
            List<ResourceCount> resourceCounts = new();
            foreach (var resourceEarner in resourceEarners)
            {
                var data = (ResourceEarnerSystemData)resourceEarner.Data;
                inventorySystem.ChangeRecourseAmount(data.Resource.ResourceName, data.AmountPerSecond);

                var existingResource = resourceCounts.Find(rc => rc.Resource == data.Resource);
                if (existingResource != null)
                {
                    existingResource.Count += Mathf.RoundToInt(data.AmountPerSecond);
                }
                else
                {
                    resourceCounts.Add(new ResourceCount
                    {
                        Resource = data.Resource,
                        Count = Mathf.RoundToInt(data.AmountPerSecond)
                    });
                }
            }
            OnResourceEarned?.Invoke(resourceCounts);
        }
    }
}