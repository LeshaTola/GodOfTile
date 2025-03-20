using System;
using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.Inventory.DTO;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.ResourceEarners
{
    public interface IResourceEarnerService
    {
        event Action<List<ResourceCount>> OnResourceEarned;
        
        bool Active { get; set; }

        void AddResourceEarnerSystem(ResourceEarner resourceEarner);
        void RemoveResourceEarnerSystem(ResourceEarner resourceEarner);
    }
}