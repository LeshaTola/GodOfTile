using System;
using UnityEngine;

namespace TileSystem
{
	public class Tile : MonoBehaviour
	{
		public event Action<TileState> OnTileStateChanged;

		[field: SerializeField] public Vector2Int Size { get; private set; } = Vector2Int.one;

		private TileState state;

		private void Start()
		{
			ChangeState(TileState.Default);
		}

		/*private void OnDrawGizmosSelected()
		{
			for (int x = 0; x < Size.x; x++)
			{
				for (int y = 0; y < Size.y; y++)
				{
					if ((x + y) % 2 == 0)
					{
						Gizmos.color = new Color(0.286f, 0.835f, 0.470f, 0.8f);
					}
					else
					{
						Gizmos.color = new Color(0.835f, 0.290f, 0.325f, 0.8f);
					}

					Gizmos.DrawCube(transform.position + new Vector3(x, 0, y), new Vector3(1.1f, 0.2f, 1.1f));
				}
			}
		}*/

		public void ChangeState(TileState state)
		{
			if (this.state != state)
			{
				this.state = state;
				OnTileStateChanged?.Invoke(state);
			}
		}

	}
}
