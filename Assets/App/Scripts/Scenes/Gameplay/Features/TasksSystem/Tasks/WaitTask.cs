using App.Scripts.Modules.Tasks.Tasks;
using App.Scripts.Modules.TimeProvider;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.TasksSystem.Tasks
{
    public class WaitTask : Task
    {
        [SerializeField] private float seconds;

        private float timer;
        
        private readonly ITimeProvider timeProvider;
        
        public WaitTask(ITimeProvider timeProvider)
        {
            this.timeProvider = timeProvider;
        }

        public override void Start()
        {
            Update().Forget();
        }

        private async UniTaskVoid Update()
        {
            while (timer <= seconds)
            {
                await UniTask.Yield();
                timer += timeProvider.DeltaTime;
                Progress = timer / seconds;
            }
        }
        
        public override void Import(Task original)
        {
            var concreteTask = (WaitTask) original;
            seconds = concreteTask.seconds;
        }
    }
}