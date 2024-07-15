using System.Collections.Generic;

namespace App.Scripts.Modules.StateMachine.Services.UpdateService
{
    public class UpdateService : IUpdateService
    {
        public List<IUpdatable> Updatables { get; private set; }

        public void Update()
        {
            foreach (var updatable in Updatables)
            {
                updatable.Update();
            }
        }
    }
}