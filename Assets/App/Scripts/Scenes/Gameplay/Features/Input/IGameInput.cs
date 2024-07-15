using System;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Input
{
    public interface IGameInput
    {
        event Action OnBuild;
        event Action OnRotate;

        Vector2Int GetGridMousePosition();
        float GetRotationValueNormalized();
        Vector2 GetMoveVectorNormalized();
        bool IsMouseClicked();
    }
}