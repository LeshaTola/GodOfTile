using System;

namespace App.Scripts.Modules.Tasks.Tasks
{
    public abstract class Task : ITask
    {
        public event Action<ITask> OnTaskCompleted;
        public event Action<float> OnProgressChanged;

        private float progress;

        public float Progress
        {
            get => progress;
            protected set
            {
                if (progress.Equals(value))
                {
                    return;
                }
                
                progress = value;
                OnProgressChanged?.Invoke(progress);

                if (progress >= 1f)
                {
                }
            }
        }

        public virtual void Complete()
        {
            OnTaskCompleted?.Invoke(this);
        }

        public abstract void Import(Task original);
    }

}