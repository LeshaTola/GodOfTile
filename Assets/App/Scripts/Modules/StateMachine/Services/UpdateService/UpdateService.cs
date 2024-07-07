using System.Collections.Generic;
using Features.StateMachineCore;

namespace Modules.StateMachine.Services.UpdateService
{
    public class UpdateService : IUpdateService
    {
        public List<IUpdatable> Updatables { get; private set; }

        public void Update()
        {
            foreach (IUpdatable updatable in Updatables)
            {
                updatable.Update();
            }
        }
    }
}
