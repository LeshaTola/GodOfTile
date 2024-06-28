using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using TileSystem;
using UnityEngine;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Tiles
{

	public class TileVisual : MonoBehaviour
	{
		[SerializeField] private TileVisualConfig config;

		[Header("Renderer")]
		[SerializeField] private MeshRenderer meshRenderer;

		private Material defaultMaterial;

		public void Initialize()
		{
			defaultMaterial = meshRenderer.material;
		}

		public void SetState(TileState state)
		{
			switch (state)
			{
				case TileState.Default: meshRenderer.material = defaultMaterial; break;
				case TileState.Wrong: meshRenderer.material = config.WrongMaterial; break;
				case TileState.Correct: meshRenderer.material = config.CorrectMaterial; break;
			}
		}
	}

}