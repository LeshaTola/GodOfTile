using System;
using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.Inventory.DTO;

namespace App.Scripts.Scenes.Gameplay.Features.Inventory.Systems
{
    public interface IInventorySystem
    {
        Dictionary<string, float> Resources { get; }

        event Action<string, float> OnRecourseAmountChanged;

        void ChangeRecourseAmount(string resourceName, float amount);
        void InitializeResources();
        bool IsEnough(string resourceName, float amount);
        bool IsEnough(List<ResourceCount> resourcesCounts);
        bool IsEnough(ResourceCount resourceCount);
    }
}