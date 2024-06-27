using UnityEngine;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Camera.Configs
{
	public interface ICameraController
	{
		void Focus();
		void Move(Vector2 dir);
	}
}