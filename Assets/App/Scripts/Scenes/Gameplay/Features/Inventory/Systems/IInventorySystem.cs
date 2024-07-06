using System;
using System.Collections.Generic;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Inventory.Systems
{
    public interface IInventorySystem
    {
        Dictionary<string, int> Resources { get; }

        event Action<string, int> OnRecourseAmountChanged;

        void ChangeRecourseAmount(string resourceName, int amount);
        void InitializeResources();
        bool IsEnough(string resourceName, int amount);
    }
}
