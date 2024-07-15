using System;
using System.Collections.Generic;
using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles;
using Features.StateMachineCore;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Services
{
    public class SystemsService : ISystemsService, IUpdatable
    {
        public List<ITileSystem> Systems { get; } = new();

        public event Action<ITileSystem> OnSystemStart;
        public event Action<ITileSystem> OnSystemUpdate;
        public event Action<ITileSystem> OnSystemStop;

        public void StartSystem(ITileSystem tileSystem)
        {
            tileSystem.Start();
            Systems.Add(tileSystem);
            OnSystemStart?.Invoke(tileSystem);
        }

        public void StartSystems(Tile tile)
        {
            foreach (var system in tile.Systems)
            {
                StartSystem(system);
            }
        }

        public void UpdateSystems()
        {
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

        public void StopSystems(Tile tile)
        {
            foreach (var system in tile.Systems)
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
