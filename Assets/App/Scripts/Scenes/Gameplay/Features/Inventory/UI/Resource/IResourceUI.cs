using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Inventory.UI.Resource
{
    public interface IResourceUI
    {
        void Initialize(Sprite sprite, int amount);
        void UpdateAmount(int amount);
    }
}