using System.Collections.Generic;
using TileSystem;
using UnityEngine;

public class MainTilesUI : MonoBehaviour
{
	[Header("Tiles")]
	[SerializeField] private List<TileSO> mainTilesSO;
	[SerializeField] private TileGrid grid;
	[Header("UI")]
	[SerializeField] private MainTileExampleUI exampleUI;
	[SerializeField] private Transform container;

	private void Start()
	{
		UpdateUI();
	}

	private void UpdateUI()
	{
		while (container.childCount > 0)
		{
			Destroy(container.GetChild(0).gameObject);
		}

		foreach (TileSO tileSO in mainTilesSO)
		{
			var tileUI = Instantiate(exampleUI, container);
			tileUI.UpdateImage(tileSO.Sprite);
			tileUI.AddListenerCreateButton(() => { grid.StartPlacingTile(tileSO.Prefab); });
		}
	}
}
