using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using UnityEngine;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Creation.Services
{
	public struct TileToUpdate
	{
		public Vector2Int Position;
		public TileConfig NewConfig;
	}
}
