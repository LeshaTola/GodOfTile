using System;
using UnityEngine;

public interface IGameInput
{
    event Action OnBuild;

    Vector3 GetGroundMousePosition();
    Vector2 GetMoveVectorNormalized();
}
