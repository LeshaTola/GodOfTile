using System;
using System.Linq;
using App.Scripts.Modules.Tasks.Configs;

namespace App.Scripts.Modules.Tasks.Tasks
{
    public class TasksContainer
    {
        public event Action<TasksContainer> OnTaskCompleted;
        public event Action<float> OnProgressChanged;

        public TaskConfig Config { get; }

        public float Progress { get; private set; }

        public TasksContainer(TaskConfig config)
        {
            this.Config = config;
            
            foreach (var configTask in config.Tasks)
            {
                configTask.OnProgressChanged += OnConfigProgressChanged;
                configTask.OnTaskCompleted += OnConfigTaskCompleted;
            }
        }

        public void CompleteTask()
        {
            foreach (var configTask in Config.CompleteActions)
            {
                configTask.Execute();
            }
            OnTaskCompleted?.Invoke(this);
        }
            
            
        private void OnConfigProgressChanged(float progress)
        {
            if (Config == null || Config.Tasks.Count == 0)
            {
                return;
            }
    
            Progress = Config.Tasks.Average(task => task.Progress);
            OnProgressChanged?.Invoke(Progress);
            if (progress.Equals(1))
            {
                CompleteTask();
            }
        }

        private void OnConfigTaskCompleted(ITask completedTask)
        {
            completedTask.OnProgressChanged -= OnConfigProgressChanged;
            completedTask.OnTaskCompleted -= OnConfigTaskCompleted;
        }
    }
}