using System.Collections.Generic;
using App.Scripts.Modules.Tasks.Tasks;
using App.Scripts.Scenes.Gameplay.Features.Inventory.Configs;
using App.Scripts.Scenes.Gameplay.Features.Map.Providers.Chunk;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.TasksSystem.Tasks.Tutorial
{
    public class BuyAreaTask : Task
    {
        [SerializeField] private List<Vector2Int> ids = new();
        [SerializeField] private int count = 1;

        private readonly IChunksProvider chunksProvider;

        private int progressCount = 0;
        private int maxCount = 0;

        public BuyAreaTask(IChunksProvider chunksProvider)
        {
            this.chunksProvider = chunksProvider;
        }

        public override void Start()
        {
            SetupMaxCount();

            chunksProvider.OnChunkOpened += OnChunkOpened;
        }

        public override void Complete()
        {
            base.Complete();

            chunksProvider.OnChunkOpened -= OnChunkOpened;
        }

        private void OnChunkOpened(Vector2Int id)
        {
            if (ids == null || ids.Count < 1)
            {
                progressCount++;
                UpdateProgress();
                return;
            }

            ExecuteForIds(id);
            UpdateProgress();
        }

        private void UpdateProgress()
        {
            Progress = ((float) progressCount) / maxCount;
        }

        private void SetupMaxCount()
        {
            if (ids == null || ids.Count < 1)
            {
                maxCount = count;
            }
            else
            {
                maxCount = ids.Count;
            }
        }

        private void ExecuteForIds(Vector2Int id)
        {
            foreach (var vector2Int in ids)
            {
                if (vector2Int.Equals(id))
                {
                    progressCount++;
                    break;
                }
            }
        }

        public override void Import(Task original)
        {
            var concreteTask = (BuyAreaTask) original;
            count = concreteTask.count;
            ids = concreteTask.ids;
        }
    }
}