using Features.StateMachineCore;
using System;
using UnityEngine;

public class GameInput : IGameInput, ICleanupable
{
	public event Action OnBuild;

	private Input input;

	public GameInput()
	{
		input = new Input();
		input.Game.Enable();

		input.Game.Build.performed += OnBuildButtonPerformed;
	}

	public Vector2 GetMoveVectorNormalized()
	{
		Vector2 moveVector;
		moveVector = input.Game.Move.ReadValue<Vector2>();
		return moveVector.normalized;
	}

	private void OnBuildButtonPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	{
		OnBuild?.Invoke();
	}

	public void Cleanup()
	{
		input.Game.Build.performed -= OnBuildButtonPerformed;
	}
}
