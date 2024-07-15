using App.Scripts.Scenes.Gameplay.Features.Inventory.UI.Resource;

namespace App.Scripts.Scenes.Gameplay.Features.Inventory.Factories
{
    public interface IResourceUIFactory
    {
        IResourceUI GetRecourseUI(string resourceName);
    }
}