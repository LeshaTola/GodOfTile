using App.Scripts.Modules.StateMachine.Services.UpdateService;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Сataclysms.Providers
{
    public abstract class Cataclysm: MonoBehaviour, IUpdatable
    {
        private CataclysmConfig config;

        public abstract void Attack(Vector2Int position);
        public abstract void Update();
    }
}