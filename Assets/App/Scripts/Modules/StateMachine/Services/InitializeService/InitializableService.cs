using System.Collections.Generic;

namespace App.Scripts.Modules.StateMachine.Services.InitializeService
{
    public class InitializeService : IInitializeService
    {
        public InitializeService(List<IInitializable> initializables)
        {
            Initializables = initializables;
        }

        public List<IInitializable> Initializables { get; private set; }

        public void Initialize()
        {
            foreach (var Initializable in Initializables)
            {
                Initializable.Initialize();
            }
        }
    }
}