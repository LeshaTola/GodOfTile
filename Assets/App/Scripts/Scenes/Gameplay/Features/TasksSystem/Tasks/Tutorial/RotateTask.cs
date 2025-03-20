using System.Collections.Generic;
using App.Scripts.Modules.Tasks.Tasks;
using App.Scripts.Scenes.Gameplay.Features.Input;
using Cysharp.Threading.Tasks;

namespace App.Scripts.Scenes.Gameplay.Features.TasksSystem.Tasks
{
    public class RotateTask : Task
    {
        private readonly IGameInput input;

        private HashSet<float> target = new()
        {
            -1,
            1
        };

        private HashSet<float> pressed = new();


        public RotateTask(IGameInput input)
        {
            this.input = input;
        }
        
        public override void Start()
        {
            Update().Forget();
        }

        public async UniTaskVoid Update()
        {
            while (Progress < 1)
            {
                await UniTask.Yield();

                var vector = input.GetRotationValueNormalized();
                if (target.Contains(vector) && !pressed.Contains(vector))
                {
                    pressed.Add(vector);
                    Progress += 1f / target.Count;
                }
            }
        }
    }
}