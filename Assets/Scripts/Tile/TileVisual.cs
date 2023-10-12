using UnityEngine;

namespace TileSystem
{
	public enum TileState
	{
		Default,
		Wrong,
		Correct
	}

	public class TileVisual : MonoBehaviour
	{
		[Header("Materials")]
		[SerializeField] private Material wrongMaterial;
		[SerializeField] private Material correctMaterial;

		[Header("Renderer")]
		[SerializeField] private MeshRenderer meshRenderer;

		[Header("Tile")]
		[SerializeField] private Tile tile;

		private Material defaultMaterial;

		private void Start()
		{
			defaultMaterial = meshRenderer.material;

			tile.OnTileStateChanged += OnTileStateChanged;
		}

		private void OnDestroy()
		{
			tile.OnTileStateChanged -= OnTileStateChanged;
		}

		public void SetMaterial(TileState state)
		{
			switch (state)
			{
				case TileState.Default: meshRenderer.material = defaultMaterial; break;
				case TileState.Wrong: meshRenderer.material = wrongMaterial; break;
				case TileState.Correct: meshRenderer.material = correctMaterial; break;
			}
		}

		private void OnTileStateChanged(TileState state)
		{
			SetMaterial(state);
		}
	}

}