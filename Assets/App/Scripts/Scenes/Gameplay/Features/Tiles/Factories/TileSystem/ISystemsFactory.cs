using System.Collections.Generic;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Factories.TileSystem
{
	public interface ISystemsFactory
	{
		TileSystems.TileSystem GetSystem(TileSystems.TileSystem original);
		List<TileSystems.TileSystem> GetSystems(List<TileSystems.TileSystem> originals);
	}
}