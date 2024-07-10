using System;
using Assets.App.Scripts.Scenes.Gameplay.Features.Inventory.Configs;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Inventory.DTO
{
    [Serializable]
    public class ResourceCount
    {
        public ResourceConfig Resource;
        public int Count;
    }
}
