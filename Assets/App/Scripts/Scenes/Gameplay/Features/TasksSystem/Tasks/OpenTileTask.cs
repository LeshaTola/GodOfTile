using System.Linq;
using App.Scripts.Modules.Tasks.Tasks;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Providers.Collection;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.TasksSystem.Tasks
{
    public class OpenTileTask : Task
    {
        [SerializeField] private TileConfig tileConfig;
        
        private readonly ITileCollectionProvider tileCollectionProvider;

        public OpenTileTask(ITileCollectionProvider tileCollectionProvider)
        {
            this.tileCollectionProvider = tileCollectionProvider;
        }

        public override void Start()
        {
            var tile = tileCollectionProvider.Collection.FirstOrDefault(x=>x.Id.Equals(tileConfig.Id));
            if (tile != null)
            {
                Progress = 1;
                return;
            }
            tileCollectionProvider.OnNewTileAdd += OnNewTileAdd;
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
        }

        private void OnNewTileAdd(TileConfig addedTile)
        {
            if (addedTile.Id.Equals(tileConfig.Id))
            {
                Progress = 1;
            }
        }
    }
}