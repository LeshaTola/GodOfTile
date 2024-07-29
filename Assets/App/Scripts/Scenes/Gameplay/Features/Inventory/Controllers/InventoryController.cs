using App.Scripts.Modules.StateMachine.Services.CleanupService;
using App.Scripts.Scenes.Gameplay.Features.Inventory.Factories;
using App.Scripts.Scenes.Gameplay.Features.Inventory.Systems;
using App.Scripts.Scenes.Gameplay.Features.Inventory.UI.Inventory;

namespace App.Scripts.Scenes.Gameplay.Features.Inventory.Controllers
{
    public class InventoryController : IInventoryController, ICleanupable
    {
        private IInventorySystem inventorySystem;
        private IInventoryUI inventoryUI;
        private IResourceUIFactory resourceUIFactory;

        public InventoryController(
            IInventorySystem inventorySystem,
            IInventoryUI inventoryUI,
            IResourceUIFactory resourceUIFactory
        )
        {
            this.inventorySystem = inventorySystem;
            this.inventoryUI = inventoryUI;
            this.resourceUIFactory = resourceUIFactory;

            inventorySystem.OnRecourseAmountChanged += OnRecourseAmountChanged;
            inventorySystem.InitializeResources();
        }

        private void OnRecourseAmountChanged(string resourceName, float amount)
        {
            if (!inventoryUI.HasRecourseUI(resourceName))
            {
                AddResourceUI(resourceName, (int) amount);
                return;
            }

            inventoryUI.UpdateResource(resourceName, (int) amount);
        }

        public void Cleanup()
        {
            inventorySystem.OnRecourseAmountChanged -= OnRecourseAmountChanged;
        }

        private void AddResourceUI(string resourceName, int amount)
        {
            var resourceUI = resourceUIFactory.GetRecourseUI(resourceName);
            resourceUI.UpdateAmount(amount);
            inventoryUI.AddRecourse(resourceName, resourceUI);
        }
    }
}