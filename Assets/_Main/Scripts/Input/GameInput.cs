using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{

	public event Action OnBuild;
	public event Action OnRotate;

	private Input input;

	public static GameInput Instance { get; private set; }

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
	}

	private void Start()
	{
		input = new Input();
		input.Game.Enable();

		input.Game.Build.performed += OnBuildButtonPerformed;
		input.Game.Rotate.performed += RotateButtonPerformed;
	}

	private void OnDestroy()
	{
		input.Game.Build.performed -= OnBuildButtonPerformed;
		input.Game.Rotate.performed -= RotateButtonPerformed;
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

	private void RotateButtonPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	{
		OnRotate?.Invoke();
	}
}
