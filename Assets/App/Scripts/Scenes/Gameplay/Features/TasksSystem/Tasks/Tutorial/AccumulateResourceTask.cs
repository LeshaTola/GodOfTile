using App.Scripts.Modules.Tasks.Tasks;
using App.Scripts.Scenes.Gameplay.Features.Inventory.DTO;
using App.Scripts.Scenes.Gameplay.Features.Inventory.Systems;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.TasksSystem.Tasks.Tutorial
{
    public class AccumulateResourceTask : Task
    {
        [SerializeField] private ResourceCount resourceCount;
        
        private readonly IInventorySystem inventorySystem;
        
        public AccumulateResourceTask(IInventorySystem inventorySystem)
        {
            this.inventorySystem = inventorySystem;
        }

        public override void Start()
        {
            base.Start();
            inventorySystem.OnRecourseAmountChanged += OnRecourseAmountChanged;
        }

        public override void Complete()
        {
            base.Complete();            
            inventorySystem.OnRecourseAmountChanged -= OnRecourseAmountChanged;
        }

        public override void Import(Task original)
        {
            var concreteTask = (AccumulateResourceTask) original;
            resourceCount = concreteTask.resourceCount;
        }

        private void OnRecourseAmountChanged(string resource, float count)
        {
            if (resource.Equals(resourceCount.Resource.ResourceName))
            {
                Progress = count/resourceCount.Count;
            }
        }
    }
}