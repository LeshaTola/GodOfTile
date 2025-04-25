using System;
using System.Collections.Generic;
using System.Linq;
using App.Scripts.Modules.Saves;
using App.Scripts.Modules.StateMachine.Services.UpdateService;
using App.Scripts.Modules.TasksSystem.Configs;
using App.Scripts.Modules.TasksSystem.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace App.Scripts.Modules.TasksSystem.Providers
{
    public class TasksProvider : ISavable
    {
        public event Action<Dictionary<string, TaskContainerData>> OnTasksUpdated;
        public event Action<Dictionary<string, TaskContainerData>> OnTasksProgressUpdated;
        
        private readonly TaskProviderConfig _config;
        private readonly IDataProvider<TasksData> _dataProvider;
        
        private int _lastTaskId;

        public Dictionary<string, TaskContainerData> ActiveTasks { get; } = new();

        public TasksProvider(TaskProviderConfig config, IDataProvider<TasksData> dataProvider)
        {
            _config = config;
            _dataProvider = dataProvider;
        }

        public void UpdateTasks()
        {
            FillTasks();
            SaveState();
        }

        public void RemoveTask(string configId)
        {
            if (!ActiveTasks.ContainsKey(configId))
            {
                return;
            }
            
            ActiveTasks.Remove(configId);
            UpdateTasks();            
        }

        public void UpdateProgress(TasksContainer tasksContainer)
        {
            if (!ActiveTasks.TryGetValue(tasksContainer.Config.Id, out var containerData))
            {
                return;
            }
            containerData.Progress = tasksContainer.GetProgress();
            containerData.TasksData = tasksContainer.Config.Tasks.Select(x=>x.GetProgress()).ToList();
            OnTasksProgressUpdated?.Invoke(ActiveTasks);
        }

        public void SaveState()
        {
            _dataProvider.SaveData(GetState());
        }

        public void LoadState()
        {
            if (!_dataProvider.HasData())
            {
                UpdateTasks();
                return;
            }
            SetState();
        }

        private void SetState()
        {
            ActiveTasks.Clear();
            var data = _dataProvider.GetData();
            _lastTaskId = data.TaskId;
            foreach (var task in data.Tasks)
            {
                AddTask(task);
            }
            OnTasksUpdated?.Invoke(ActiveTasks);
        }

        private TasksData GetState()
        {
            return new TasksData()
            {
                TaskId = _lastTaskId,
                Tasks = ActiveTasks.Values.ToList()
            };
        }

        private void FillTasks()
        {
            ActiveTasks.Clear();
            for (int i = 0; i < _config.MaxTasksCount; i++)
            {
                GetTask();
            }
            OnTasksUpdated?.Invoke(ActiveTasks);
        }

        private void GetTask()
        {
            TaskConfig config;
            do
            {
                var id = GetConfigId();
                config = _config.TasksPool.Tasks[id];
            } while (ActiveTasks.ContainsKey(config.Id));

            AddTask(config);
        }

        private void AddTask(TaskConfig config)
        {
            ActiveTasks.Add(config.Id, new()
            {
                TaskConfig = config,
                TaskConfigId = config.Id,
                TasksData = null,
                Progress = config.Tasks.Count >= 2 ? new (0, 1) : config.Tasks[0].GetProgress()
            });
        }
        
        private void AddTask(TaskContainerData taskData)
        {
            taskData.TaskConfig = _config.TasksPool.Tasks.FirstOrDefault(x=>x.Id.Equals(taskData.TaskConfigId));
            ActiveTasks.Add(taskData.TaskConfigId, taskData);
        }

        private int GetConfigId()
        {
            var id = _config.IsRandom ? Random.Range(0, _config.TasksPool.Tasks.Count) : _lastTaskId++;
            Debug.Log($"lastTaskId: {_lastTaskId}");
            if (id >= _config.TasksPool.Tasks.Count)
            {
                id = 0;
                _lastTaskId = 0;
            }
            return id;
        }
    }
}
