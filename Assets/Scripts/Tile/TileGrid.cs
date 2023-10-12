using UnityEngine;

namespace TileSystem
{
	public class TileGrid : MonoBehaviour
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
			for (int x = 0; x < maxGridSize.x; x++)
			{
				for (int y = 0; y < maxGridSize.y; y++)
				{
					var tile = Instantiate(tileToSpawn, transform);
					tile.transform.position = new Vector3(x, 0, y);
				}
			}
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.V))
			{
				StartPlacingTile(tileToSpawn);
			}

			if (activeTile != null)
			{
				var ground = new Plane(Vector3.up, Vector3.zero);
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

				if (ground.Raycast(ray, out float position))
				{
					var worldPosition = ray.GetPoint(position);

					var roundedWorldPosition = new Vector3(Mathf.RoundToInt(worldPosition.x), 0f, Mathf.RoundToInt(worldPosition.z));
					activeTile.transform.position = roundedWorldPosition;

					if (IsValid(activeTile))
					{
						activeTile.ChangeState(TileState.Correct);

						if (Input.GetMouseButtonDown(0))
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
					tiles[(int)activeTile.transform.position.x + x, (int)activeTile.transform.position.z + y] = activeTile;
				}
			}
			activeTile = null;
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

	}
}