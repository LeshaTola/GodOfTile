using Features.StateMachineCore;
using System.Collections.Generic;

namespace Modules.StateMachine.Services.UpdateService
{
	public interface IUpdateService
	{
		List<IUpdatable> Updatables { get; }

		void Update();
	}
}