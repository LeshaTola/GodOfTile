using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.Researches.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.ResourceEarners;

namespace App.Scripts.Modules.StateMachine.Services.UpdateService
{
    public interface IUpdatable
    {
        public void Update();
    }
}