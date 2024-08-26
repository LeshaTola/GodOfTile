using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.CameraLogic
{
    public interface ICameraController
    {
        bool Active { get; set; }

        void Focus();
        void Move(Vector2 dir);
        void Rotate(float value);
    }
}