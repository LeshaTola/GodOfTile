using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.Inventory.DTO;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Popups.BuyAreaPopup.Routers
{
    public interface IBuyAreaPopupRouter
    {
        UniTask Hide();
        UniTask Show(Vector2Int chunkId);
    }
}