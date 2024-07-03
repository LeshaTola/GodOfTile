using System;
using Assets.App.Scripts.Scenes.Gameplay.Features.Grid;
using Features.StateMachineCore;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : IGameInput, ICleanupable
{
    public event Action OnBuild;
    public event Action OnRotate;

    private Input input;
    private Camera mainCamera;
    private IGridProvider gridProvider;

    private Plane ground;

    public GameInput(Camera mainCamera, IGridProvider gridProvider)
    {
        this.mainCamera = mainCamera;

        input = new Input();
        input.Game.Enable();

        input.Game.Build.performed += BuildButtonPerformed;
        input.Game.Rotate.performed += RotationButtonPerformed;

        ground = new(Vector2.up, Vector3.zero);
        this.gridProvider = gridProvider;
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

    public Vector2Int GetGridMousePosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (!ground.Raycast(ray, out float position))
        {
            return default;
        }

        var worldPosition = ray.GetPoint(position);
        Vector2Int gridPosition = new Vector2Int(
            Mathf.RoundToInt(worldPosition.x),
            Mathf.RoundToInt(worldPosition.z)
        );

        return gridPosition;
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
