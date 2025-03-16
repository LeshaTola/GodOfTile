using System.Collections.Generic;
using System.Linq;
using App.Scripts.Modules.Tasks.CompleteActions;
using App.Scripts.Scenes.Gameplay.Features.Inventory.DTO;
using App.Scripts.Scenes.Gameplay.Features.Inventory.Systems;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.TasksSystem.CompleteActions
{
    public class AddResources : CompleteAction
    {
        [SerializeField] private List<ResourceCount> rewardResources;

        private readonly IInventorySystem inventorySystem;

        public AddResources(IInventorySystem inventorySystem)
        {
            this.inventorySystem = inventorySystem;
        }

        public override void Execute()
        {
            foreach (var reward in rewardResources)
            {
                inventorySystem.ChangeRecourseAmount(reward.Resource.ResourceName, reward.Count);
            }
        }

        public override void Import(CompleteAction original)
        {
            var concrete =(AddResources) original;
            rewardResources = concrete.rewardResources;
        }

        public override List<RewardData> GetRewardData()
        {
            return rewardResources.Select(x => new RewardData
            {
                Sprite = x.Resource.Sprite,
                Text = x.Count.ToString()
            }).ToList();
        }
    }
}