using System.Collections.Generic;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Factories
{
	public interface ISystemsFactory
	{
		TileSystem GetSystem(TileSystem original);
		List<TileSystem> GetSystems(List<TileSystem> originals);
	}
}