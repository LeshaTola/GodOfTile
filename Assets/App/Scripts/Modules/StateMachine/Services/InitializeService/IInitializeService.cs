using System.Collections.Generic;

namespace Modules.StateMachine.Services.InitializeService
{
	public interface IInitializeService
	{
		List<IInitializable> Initializables { get; }

		void Initialize();
	}
}