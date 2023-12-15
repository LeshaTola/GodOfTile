using CraftingSystem;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace TileSystem
{
	internal class TileGrid : MonoBehaviour
	{
		[SerializeField] private Vector2Int maxGridSize = new Vector2Int(10, 10);
		[SerializeField] private Tile tileToSpawn;

		private Tile[,] tiles;
		private Tile activeTile = null;

		private void Awake()
		{
			tiles = new Tile[maxGridSize.x, maxGridSize.y];
		}

		private void Start()
		{
			GameInput.Instance.OnBuild += OnBuild;
			//FullFill();
		}

		private void OnDestroy()
		{
			GameInput.Instance.OnBuild -= OnBuild;
		}

		private void Update()
		{
			if (activeTile != null)
			{
				var ground = new Plane(Vector3.up, Vector3.zero);
				Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

				if (ground.Raycast(ray, out float position))
				{
					var worldPosition = ray.GetPoint(position);

					var roundedWorldPosition = new Vector3(Mathf.RoundToInt(worldPosition.x), 0f, Mathf.RoundToInt(worldPosition.z));
					activeTile.transform.position = roundedWorldPosition;

					if (IsValid(activeTile))
					{
						activeTile.ChangeState(TileState.Correct);

						if (Mouse.current.leftButton.wasPressedThisFrame)
						{
							StopPlacingTile();
						}
					}
					else
					{
						activeTile.ChangeState(TileState.Wrong);
					}
				}
			}
		}

		private void StartPlacingTile(Tile tile)
		{
			if (activeTile != null)
			{
				Destroy(activeTile.gameObject);
			}
			else
			{
				activeTile = Instantiate(tile, transform);
			}
		}

		private void StopPlacingTile()
		{
			activeTile.ChangeState(TileState.Default);
			for (int x = 0; x < activeTile.Size.x; x++)
			{
				for (int y = 0; y < activeTile.Size.y; y++)
				{
					Vector2Int tileCoordinate = new Vector2Int((int)activeTile.transform.position.x + x, (int)activeTile.transform.position.z + y);
					tiles[tileCoordinate.x, tileCoordinate.y] = activeTile;
					UpdateConnectedTiles(tileCoordinate);
				}
			}
			activeTile = null;
		}

		private List<Vector2Int> GetCoveringTiles(Vector2Int tile)
		{
			var neighbors = new List<Vector2Int>();
			for (int x = tile.x - 1; x <= tile.x + 1; x++)
			{
				for (int y = tile.y - 1; y <= tile.y + 1; y++)
				{
					if (x != tile.x || y != tile.y)
					{
						if (x >= 0 && x < maxGridSize.x && y >= 0 && y < maxGridSize.y)
						{
							neighbors.Add(new Vector2Int(x, y));
						}
					}
				}
			}
			return neighbors;
		}

		private void UpdateConnectedTiles(Vector2Int tilePosition)
		{
			var tilesPositions = GetCoveringTiles(tilePosition);
			tilesPositions.Add(tilePosition);

			List<KeyValuePair<Tile, Vector2Int>> tilesForUpdate = new();
			foreach (var position in tilesPositions)
			{
				if (tiles[position.x, position.y] != null)
				{
					var result = UpdateTile(position);
					if (result != null)
					{
						tilesForUpdate.Add(new KeyValuePair<Tile, Vector2Int>(result, position));
					}
				}
			}

			foreach (var tile in tilesForUpdate)
			{
				Replace(tile.Key, tile.Value);
			}
		}

		private Tile UpdateTile(Vector2Int tilePosition)
		{
			var neighbors = GetCoveringTiles(tilePosition);

			var allTiles = new List<Tile>();
			foreach (var neighbor in neighbors)
			{
				var tile = tiles[neighbor.x, neighbor.y];
				if (tile != null)
					allTiles.Add(tile);
			}

			return FindRecipe(allTiles, tiles[tilePosition.x, tilePosition.y]);

		}

		private void Replace(Tile tile, Vector2Int position)
		{
			var oldTile = tiles[position.x, position.y];

			var newTile = Instantiate(tile, transform);
			tiles[position.x, position.y] = newTile;

			newTile.transform.position = oldTile.transform.position;
			Destroy(oldTile.gameObject);
		}

		private Tile FindRecipe(List<Tile> neighbors, Tile tile)
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

		private bool IsValid(Tile tile)
		{
			bool result = false;
			if (activeTile != null)
			{
				result = IsInBounds(tile) && IsOccupied(tile);
			}

			return result;
		}

		private bool IsInBounds(Tile tile)
		{
			bool result;
			result = !(tile.transform.position.x > maxGridSize.x - tile.Size.x
					|| tile.transform.position.z > maxGridSize.y - tile.Size.y)
					&& !(tile.transform.position.x < 0
					|| tile.transform.position.z < 0);
			return result;
		}

		private bool IsOccupied(Tile tile)
		{
			bool result = true;
			for (int x = 0; x < activeTile.Size.x; x++)
			{
				for (int y = 0; y < activeTile.Size.y; y++)
				{
					if (tiles[(int)tile.transform.position.x + x, (int)tile.transform.position.z + y] != null)
					{
						result = false;
						break;
					}
				}
			}
			return result;

		}

		private void FullFill()
		{
			for (int x = 0; x < maxGridSize.x; x++)
			{
				for (int y = 0; y < maxGridSize.y; y++)
				{
					var tile = Instantiate(tileToSpawn, transform);
					tile.transform.position = new Vector3(x, 0, y);
				}
			}
		}

		private void OnBuild()
		{
			StartPlacingTile(tileToSpawn);
		}
	}
}