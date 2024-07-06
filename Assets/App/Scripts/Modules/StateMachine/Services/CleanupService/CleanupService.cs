using System.Collections.Generic;
using Features.StateMachineCore;

namespace Modules.StateMachine.Services.CleanupService
{
    public class CleanupService : ICleanupService
    {
        public List<ICleanupable> Cleanupables { get; private set; }

        public void Cleanup()
        {
            foreach (ICleanupable cleanupable in Cleanupables)
            {
                cleanupable.Cleanup();
            }
        }
    }
}
