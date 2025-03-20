using System.Collections.Generic;
using App.Scripts.Modules.Tasks.Tasks;
using App.Scripts.Scenes.Gameplay.Features.Input;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.TasksSystem.Tasks
{
    public class MoveTask : Task
    {
        private readonly IGameInput input;

        private HashSet<Vector2> target = new()
        {
            new Vector2(1, 0),
            new Vector2(-1, 0),
            new Vector2(0, 1),
            new Vector2(0, -1)
        };

        private HashSet<Vector2> pressed = new();
        

        public MoveTask(IGameInput input)
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

                var vector = input.GetMoveVectorNormalized();
                if (target.Contains(vector) && !pressed.Contains(vector))
                {
                    pressed.Add(vector);
                    Progress += 1f/target.Count;
                }
            }
        }
    }
}