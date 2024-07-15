using System;
using System.Collections.Generic;
using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Services
{
	public interface ISystemsService
	{
		List<ITileSystem> Systems { get; }

		event Action<ITileSystem> OnSystemStart;
		event Action<ITileSystem> OnSystemUpdate;
		event Action<ITileSystem> OnSystemStop;

		void StartSystem(ITileSystem tileSystem);
		void StartSystems(Tile tile);
		void UpdateSystems();
		void StopSystem(ITileSystem tileSystem);
		void StopSystems(Tile tile);
	}
}
