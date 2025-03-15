using System;
using System.Collections.Generic;
using App.Scripts.Modules.Tasks.Configs;
using App.Scripts.Modules.Tasks.Factories;
using App.Scripts.Modules.Tasks.Tasks;
using Random = UnityEngine.Random;

namespace App.Scripts.Modules.Tasks.Providers
{

    public class TasksProvider
    {
        public event Action<List<TasksContainer>> OnTasksUpdated;
        
        private readonly TaskProviderConfig config;
        private readonly TasksContainerFactory factory;
        
        private int lastCompletedTaskId;

        public List<TasksContainer> ActiveTasks { get; } = new();

        public TasksProvider(TaskProviderConfig config, TasksContainerFactory factory)
        {
            this.config = config;
            this.factory = factory;
        }

        public void FillTasks()
        {
            for (int i = 0; i < config.MaxTasksCount; i++)
            {
                NextTask();
            }
        }

        public void ClearTasks()
        {
            foreach (var task in ActiveTasks)
            {
                UnregisterTask(task);
            }
            ActiveTasks.Clear();
        }
        
        private void NextTask()
        {
            var id = config.IsRandom ? Random.Range(0, config.TasksPool.Tasks.Count) : lastCompletedTaskId ++;
            var tasksContainer = factory.GetTaskContainer(config.TasksPool.Tasks[id]);
            
            RegisterTask(tasksContainer);
            OnTasksUpdated?.Invoke(ActiveTasks);
        }

        private void RegisterTask(TasksContainer task)
        {
            task.OnTaskCompleted += OnTaskCompleted;
            ActiveTasks.Add(task);
        }
        
        private void UnregisterTask(TasksContainer task)
        {
            task.OnTaskCompleted -= OnTaskCompleted;
            ActiveTasks.Remove(task);
        }

        private void OnTaskCompleted(TasksContainer task)
        {
            UnregisterTask(task);
            NextTask();
        }
    }
}