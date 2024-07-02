using System;
using UnityEngine;

public interface IGameInput
{
	event Action OnBuild;
	event Action OnRotate;

	Vector3 GetGroundMousePosition();
	Vector2 GetMoveVectorNormalized();
	bool IsMouseClicked();
}
