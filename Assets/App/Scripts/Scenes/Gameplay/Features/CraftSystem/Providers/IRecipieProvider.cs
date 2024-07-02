using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using System.Collections.Generic;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.CraftSystem.Providers
{
	public interface IRecipeProvider
	{
		public TileConfig GetRecipe(List<TileConfig> neighbors, TileConfig tile);
	}
}
