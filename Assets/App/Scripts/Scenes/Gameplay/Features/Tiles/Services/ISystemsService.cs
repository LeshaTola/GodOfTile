using System;
using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Services
{
    public interface ISystemsService
    {
        List<ITileSystem> Systems { get; }

        event Action<ITileSystem> OnSystemStart;
        event Action<ITileSystem> OnSystemUpdate;
        event Action<ITileSystem> OnSystemStop;

        void StartSystem(ITileSystem tileSystem);
        void StartSystems(TileConfig tileConfig);

        void UpdateSystems();

        void StopSystem(ITileSystem tileSystem);
        void StopSystems(TileConfig tileConfig);
    }
}