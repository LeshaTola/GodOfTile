using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.Tiles.General;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Factories.TileSystem
{
	public interface ISystemsFactory
	{
		TileSystems.TileSystem GetSystem(TileSystems.TileSystem original,Tile parent);
		List<TileSystems.TileSystem> GetSystems(List<TileSystems.TileSystem> originals,Tile parent);
	}
}