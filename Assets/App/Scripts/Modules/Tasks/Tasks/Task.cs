using System;

namespace App.Scripts.Modules.Tasks.Tasks
{
    public abstract class Task : ITask
    {
        public event Action<ITask> OnTaskCompleted;
        public event Action<float> OnProgressChanged;

        public float Progress { get; private set; }
        
        public abstract void Import(Task original);
    }
}