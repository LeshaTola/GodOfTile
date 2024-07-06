namespace Assets.App.Scripts.Scenes.Gameplay.Features.Inventory.UI
{
    public interface IInventoryUI
    {
        void AddRecourse(string recourseName, IResourceUI UI);
        bool HasRecourseUI(string recourseName);
        void UpdateResource(string recourseName, int recourseAmount);
    }
}
