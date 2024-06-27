using System;
using UnityEngine;

public interface IGameInput
{
	event Action OnBuild;

	Vector2 GetMoveVectorNormalized();
}