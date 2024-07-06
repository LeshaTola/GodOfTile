using UnityEngine;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Inventory.UI
{
    public interface IResourceUI
    {
        void Initialize(Sprite sprite, int amount);
        void UpdateAmount(int amount);
    }
}
