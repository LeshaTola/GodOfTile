using System;
using System.Collections.Generic;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Configs.Systems.General.Services
{
    public interface ISystemsService
    {
        List<ISystem> Systems { get; }

        event Action<ISystem> OnSystemStart;
        event Action<ISystem> OnSystemUpdate;
        event Action<ISystem> OnSystemStop;

        void StartSystem(ISystem system);
        void UpdateSystems();
        void StopSystem(ISystem system);
    }
}