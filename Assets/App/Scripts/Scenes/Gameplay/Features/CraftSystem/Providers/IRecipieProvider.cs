using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using CraftingSystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.CraftSystem.Providers
{
	public interface IRecipeProvider
	{
		public TileConfig GetRecipe(List<TileConfig> neighbors, TileConfig tile);
	}

	public class RecipeProvider : IRecipeProvider
	{
		public TileConfig GetRecipe(List<TileConfig> neighbors, TileConfig tile)
		{
			var recipes = Resources.LoadAll<RecipeSO>("Recipes");

			var recipesForOrigin = recipes.Where(r => r.Original.GetType().Equals(tile.GetType())).ToList();
			foreach (var recipe in recipesForOrigin)
			{
				var ingredientTypes = recipe.RequiredTiles.Select(t => t.GetType()).ToList();
				var neighborsTypes = neighbors.Select(t => t.GetType()).ToList();
				if (ingredientTypes.All(x => neighborsTypes.Count(y => y == x) >= ingredientTypes.Count(y => y == x)))
				{
					return recipe.Result;
				}
			}
			return null;
		}
	}
}
