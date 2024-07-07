using Features.StateMachineCore;
using System.Collections.Generic;

namespace Modules.StateMachine.Services.CleanupService
{
	public interface ICleanupService
	{
		List<ICleanupable> Cleanupables { get; }

		void Cleanup();
	}
}