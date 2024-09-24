using System;
using App.Scripts.Scenes.Gameplay.Features.Inventory.Configs;
using Sirenix.OdinInspector;

namespace App.Scripts.Scenes.Gameplay.Features.Inventory.DTO
{
    [Serializable]
    [InlineProperty] [HideLabel]
    public class ResourceCount
    {
        public ResourceConfig Resource;
        public int Count;
    }
}