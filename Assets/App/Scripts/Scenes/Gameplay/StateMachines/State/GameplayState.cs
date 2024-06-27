using Features.StateMachineCore;
using Features.StateMachineCore.States;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.App.Scripts.Scenes.Gameplay.StateMachines.States
{
	public class GameplayState : State
	{
		private List<IUpdatable> updatables;
		public GameplayState(List<IUpdatable> updatables, string id) : base(id)
		{
			this.updatables = updatables;
		}

		public override void Enter()
		{
			base.Enter();
			Debug.Log("gameplayState");
		}

		public override void Update()
		{
			base.Update();

			foreach (var updatable in updatables)
			{
				updatable.Update();
			}
		}
	}
}
