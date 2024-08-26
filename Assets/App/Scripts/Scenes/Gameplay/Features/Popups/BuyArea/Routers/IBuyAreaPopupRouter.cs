using Cysharp.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Popups.BuyArea.Routers
{
    public interface IBuyAreaPopupRouter
    {
        UniTask Hide();
        UniTask Show(Vector2Int chunkId);
    }
}