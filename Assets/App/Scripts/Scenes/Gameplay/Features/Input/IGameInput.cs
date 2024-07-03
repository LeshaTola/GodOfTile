using System;
using UnityEngine;

public interface IGameInput
{
    event Action OnBuild;
    event Action OnRotate;

    Vector2Int GetGridMousePosition();
    Vector2 GetMoveVectorNormalized();
    bool IsMouseClicked();
}
