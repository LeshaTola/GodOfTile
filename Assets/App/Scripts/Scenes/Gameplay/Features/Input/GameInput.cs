using Features.StateMachineCore;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : IGameInput, ICleanupable
{
	public event Action OnBuild;
	public event Action OnRotate;

	private Input input;
	private Camera mainCamera;

	private Plane ground;

	public GameInput(Camera mainCamera)
	{
		this.mainCamera = mainCamera;

		input = new Input();
		input.Game.Enable();

		input.Game.Build.performed += BuildButtonPerformed;
		input.Game.Rotate.performed += RotationButtonPerformed;

		ground = new(Vector2.up, Vector3.zero);
	}

	public Vector2 GetMoveVectorNormalized()
	{
		Vector2 moveVector;
		moveVector = input.Game.Move.ReadValue<Vector2>();
		return moveVector.normalized;
	}

	public void Cleanup()
	{
		input.Game.Build.performed -= BuildButtonPerformed;
	}

	public bool IsMouseClicked()
	{
		return Mouse.current.leftButton.wasPressedThisFrame;
	}

	public Vector3 GetGroundMousePosition()
	{
		Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
		if (!ground.Raycast(ray, out float position))
		{
			return default;
		}

		var worldPosition = ray.GetPoint(position);
		return worldPosition;
	}

	private void BuildButtonPerformed(InputAction.CallbackContext obj)
	{
		OnBuild?.Invoke();
	}

	private void RotationButtonPerformed(InputAction.CallbackContext obj)
	{
		OnRotate?.Invoke();
	}
}
