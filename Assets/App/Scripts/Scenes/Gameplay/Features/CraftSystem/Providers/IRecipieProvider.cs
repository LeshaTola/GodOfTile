using System.Collections.Generic;
using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.CraftSystem.Providers
{
    public interface IRecipeProvider
    {
        public TileConfig GetRecipe(List<TileConfig> neighbors, TileConfig tile);
    }
}