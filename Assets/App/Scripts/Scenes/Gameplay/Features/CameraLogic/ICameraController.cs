using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.CameraLogic
{
    public interface ICameraController
    {
        void Focus();
        void Move(Vector2 dir);
        void Rotate(float value);
        void Activate();
        void Deactivate();
    }
}