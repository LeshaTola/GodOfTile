using System;

namespace App.Scripts.Modules.Tasks.Tasks
{
    public interface ITask
    {
        public event Action<ITask> OnTaskCompleted;
        public event Action<float> OnProgressChanged;
        
        public float Progress { get;}
    }
}