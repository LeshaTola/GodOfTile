using System;
using System.Collections.Generic;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Configs.Systems.General.Services
{
    public class SystemsService : ISystemsService
    {
        public List<ISystem> Systems { get; } = new();

        public event Action<ISystem> OnSystemStart;
        public event Action<ISystem> OnSystemUpdate;
        public event Action<ISystem> OnSystemStop;

        public void StartSystem(ISystem system)
        {
            system.Start();
            Systems.Add(system);
            OnSystemStart?.Invoke(system);
        }

        public void UpdateSystems()
        {
            foreach (var system in Systems)
            {
                system.Update();
                OnSystemUpdate?.Invoke(system);
            }
        }

        public void StopSystem(ISystem system)
        {
            system.Stop();
            Systems.Remove(system);
            OnSystemStop?.Invoke(system);
        }
    }
}