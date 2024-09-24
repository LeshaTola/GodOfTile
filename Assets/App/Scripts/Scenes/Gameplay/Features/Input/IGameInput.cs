using System;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Input
{
    public interface IGameInput
    {
        event Action OnEscape;
        event Action OnBuild;
        event Action OnRotate;

        event Action OnPause;
        event Action OnSpeed1;
        event Action OnSpeed2;
        event Action OnSpeed3;

        event Action OnI;
        event Action OnM;

        Vector2Int GetGridMousePosition();
        float GetRotationValueNormalized();
        Vector2 GetMoveVectorNormalized();
        float GetMouseScrollNormalized();
        bool IsMouseClicked();
    }
}