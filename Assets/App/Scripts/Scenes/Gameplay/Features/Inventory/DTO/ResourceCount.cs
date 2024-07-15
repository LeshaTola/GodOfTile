using System;
using App.Scripts.Scenes.Gameplay.Features.Inventory.Configs;

namespace App.Scripts.Scenes.Gameplay.Features.Inventory.DTO
{
    [Serializable]
    public class ResourceCount
    {
        public ResourceConfig Resource;
        public int Count;
    }
}