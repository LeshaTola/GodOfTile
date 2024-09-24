using System;
using System.Collections.Generic;
using App.Scripts.Modules.StateMachine.Services.UpdateService;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Services
{
    public class SystemsService : ISystemsService, IUpdatable
    {
        public List<ITileSystem> Systems { get; } = new();

        public event Action<ITileSystem> OnSystemStart;
        public event Action<ITileSystem> OnSystemUpdate;
        public event Action<ITileSystem> OnSystemStop;

        public bool Active { get; set; } = false;

        public void StartSystem(ITileSystem tileSystem)
        {
            tileSystem.Start();
            Systems.Add(tileSystem);
            OnSystemStart?.Invoke(tileSystem);
        }

        public void StartSystems(TileConfig tileConfig)
        {
            foreach (var system in tileConfig.ActiveSystems)
            {
                StartSystem(system);
            }
        }

        public void UpdateSystems()
        {
            if (!Active)
            {
                return;
            }

            foreach (var system in Systems)
            {
                system.Update();
                OnSystemUpdate?.Invoke(system);
            }
        }

        public void StopSystem(ITileSystem tileSystem)
        {
            tileSystem.Stop();
            Systems.Remove(tileSystem);
            OnSystemStop?.Invoke(tileSystem);
        }

        public void StopSystems(TileConfig tileConfig)
        {
            foreach (var system in tileConfig.ActiveSystems)
            {
                StopSystem(system);
            }
        }

        public void Update()
        {
            UpdateSystems();
        }
    }
}