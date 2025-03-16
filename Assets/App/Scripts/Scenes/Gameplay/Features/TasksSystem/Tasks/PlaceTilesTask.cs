using App.Scripts.Modules.Tasks.Tasks;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Services.TilesCreation;
using App.Scripts.Scenes.Gameplay.Features.Tiles.General;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.TasksSystem.Tasks
{
    public class PlaceTilesTask: Task
    {
        [SerializeField] private int count;

        private int progressCount;
            
        private readonly ITilesCreationService tilesCreationService;

        public PlaceTilesTask(ITilesCreationService tilesCreationService)
        {
            this.tilesCreationService = tilesCreationService;
            this.tilesCreationService.OnTilePlaced += OnTilePlaced;
        }

        public override void Complete()
        {
            base.Complete();
            tilesCreationService.OnTilePlaced -= OnTilePlaced;
        }

        public override void Import(Task original)
        {
            var concreteTask = (PlaceTilesTask) original;
            
            count = concreteTask.count;
        }

        private void OnTilePlaced(Vector2Int position, Tile tile)
        {
            progressCount++;
            Progress = progressCount / (float)count; 
        }
    }
}