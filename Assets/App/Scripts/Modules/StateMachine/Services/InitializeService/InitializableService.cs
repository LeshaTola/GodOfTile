using System.Collections.Generic;

namespace Modules.StateMachine.Services.InitializeService
{
    public class InitializeService : IInitializeService
    {
        public List<IInitializable> Initializables { get; private set; }

        public void Initialize()
        {
            foreach (IInitializable Initializable in Initializables)
            {
                Initializable.Initialize();
            }
        }
    }
}
