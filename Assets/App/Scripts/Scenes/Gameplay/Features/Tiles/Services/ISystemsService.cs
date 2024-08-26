using System;
using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Services
{
    public interface ISystemsService
    {
        event Action<ITileSystem> OnSystemStart;
        event Action<ITileSystem> OnSystemUpdate;
        event Action<ITileSystem> OnSystemStop;

        bool Active { get; set; }
        List<ITileSystem> Systems { get; }

        void StartSystem(ITileSystem tileSystem);
        void StartSystems(TileConfig tileConfig);

        void UpdateSystems();

        void StopSystem(ITileSystem tileSystem);
        void StopSystems(TileConfig tileConfig);
    }
}