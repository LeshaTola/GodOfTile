using System.Linq;
using App.Scripts.Modules.TasksSystem.Tasks;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Providers.Collection;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.TasksSystem.Tasks
{
    public class OpenTileTask : Task
    {
        [SerializeField] private TileConfig tileConfig;
        [SerializeField] private int count = -1 ;
        
        private readonly ITileCollectionProvider tileCollectionProvider;

        public OpenTileTask(ITileCollectionProvider tileCollectionProvider)
        {
            this.tileCollectionProvider = tileCollectionProvider;
        }

        public override void Start()
        {
            if (tileConfig != null)
            {
                var tile = tileCollectionProvider.Collection.FirstOrDefault(x=>x.Id.Equals(tileConfig.Id));
                if (tile != null)
                {
                    Progress = 1;
                    return;
                }
                tileCollectionProvider.OnNewTileAdd += OnNewTileAdd;
                return;
            }
            
            tileCollectionProvider.OnNewTileAdd += OnNewTileAdd;
            UpdateProgress();
        }

        public override ProgressPair GetProgress()
        {
            return new ProgressPair()
            {
                Progress = (int) count,
                Target = (int) count
            };
        }

        public override void SetProgress(ProgressPair progress)
        {
            count = progress.Progress;
        }

        public override void Complete()
        {
            tileCollectionProvider.OnNewTileAdd -= OnNewTileAdd;
            
            base.Complete();
        }

        public override void Import(Task original)
        {
            var concreteTask = (OpenTileTask) original;
            tileConfig = concreteTask.tileConfig;
            count = concreteTask.count;
        }

        private void OnNewTileAdd(TileConfig addedTile)
        {
            if (tileConfig != null)
            {
                if (addedTile.Id.Equals(tileConfig.Id))
                {
                    Progress = 1;
                }
                return;
            }

            UpdateProgress();
        }

        private void UpdateProgress()
        {
            Progress = (float)tileCollectionProvider.Collection.Count / count;
        }
    }
}