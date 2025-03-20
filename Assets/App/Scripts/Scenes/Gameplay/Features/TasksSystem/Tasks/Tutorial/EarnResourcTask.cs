using System.Collections.Generic;
using System.Linq;
using App.Scripts.Modules.Tasks.Tasks;
using App.Scripts.Scenes.Gameplay.Features.Inventory.DTO;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.ResourceEarners;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.TasksSystem.Tasks.Tutorial
{
    public class EarnResourcTask : Task
    {
        [SerializeField] private ResourceCount resourceCount;
        
        private IResourceEarnerService resourceEarnerService;
        
        private int progressCount = 0;

        public EarnResourcTask(IResourceEarnerService resourceEarnerService)
        {
            this.resourceEarnerService = resourceEarnerService;
        }

        public override void Start()
        {
            base.Start();
            resourceEarnerService.OnResourceEarned += OnResourceEarned;
        }

        public override void Complete()
        {
            base.Complete();
            resourceEarnerService.OnResourceEarned -= OnResourceEarned;
        }

        public override void Import(Task original)
        {
            var concreteTask = (EarnResourcTask) original;
            resourceCount = concreteTask.resourceCount;
        }

        private void OnResourceEarned(List<ResourceCount> erned)
        {
            var erncedResource = erned.FirstOrDefault(x => x.Resource.ResourceName.Equals(resourceCount.Resource.ResourceName));

            if (erncedResource == null)
            {
                return;
            }
            
            progressCount += erncedResource.Count;
            Progress = ((float)progressCount)/resourceCount.Count;
        }
    }
}