using System.Collections.Generic;
using System.Linq;
using App.Scripts.Modules.Tasks.CompleteActions;
using App.Scripts.Scenes.Gameplay.Features.Inventory.Systems;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Providers.Collection;
using App.Scripts.Scenes.Gameplay.Features.Time.UI;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.TasksSystem.CompleteActions
{
    public class OpenTileAction : CompleteAction
    {
        [SerializeField] private List<TileConfig> rewardResources;

        private readonly ITileCollectionProvider tileCollectionProvider;

        public OpenTileAction(ITileCollectionProvider tileCollectionProvider)
        {
            this.tileCollectionProvider = tileCollectionProvider;
        }

        public override void Execute()
        {
            foreach (var reward in rewardResources)
            {
                tileCollectionProvider.AddIfNotContains(reward);
            }
        }

        public override void Import(CompleteAction original)
        {
            var concrete =(OpenTileAction) original;
            rewardResources = concrete.rewardResources;
        }

        public override List<RewardData> GetRewardData()
        {
            return rewardResources.Select(x => new RewardData
            {
                Sprite = x.TileSprite,
                Text = "1"
            }).ToList();
        }
    }
}