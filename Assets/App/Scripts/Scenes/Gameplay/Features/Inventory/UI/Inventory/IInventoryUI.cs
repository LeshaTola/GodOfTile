using App.Scripts.Scenes.Gameplay.Features.Inventory.UI.Resource;

namespace App.Scripts.Scenes.Gameplay.Features.Inventory.UI.Inventory
{
    public interface IInventoryUI
    {
        void AddRecourse(string recourseName, IResourceUI UI);
        bool HasRecourseUI(string recourseName);
        void UpdateResource(string recourseName, int recourseAmount);
    }
}