using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Services
{
    public struct TileToUpdate
    {
        public Vector2Int Position;
        public TileConfig NewConfig;
    }
}