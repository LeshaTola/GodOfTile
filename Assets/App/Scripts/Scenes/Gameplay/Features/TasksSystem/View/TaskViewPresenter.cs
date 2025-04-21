using System.Collections.Generic;
using App.Scripts.Modules.Localization;
using App.Scripts.Modules.StateMachine.Services.CleanupService;
using App.Scripts.Modules.StateMachine.Services.InitializeService;
using App.Scripts.Modules.TasksSystem.Providers;
using App.Scripts.Modules.TasksSystem.Services;
using App.Scripts.Modules.TasksSystem.Tasks;

namespace App.Scripts.Scenes.Gameplay.Features.TasksSystem.View
{
    public class TaskViewPresenter : IInitializable, ICleanupable
    {
        private readonly TaskView view;
        private readonly TaskService taskService;
        private readonly ILocalizationSystem localizationSystem;

        private TasksContainer currentTask;

        public TaskViewPresenter(TaskView view,
            TaskService taskService,
            ILocalizationSystem localizationSystem)
        {
            this.view = view;
            this.taskService = taskService;
            this.localizationSystem = localizationSystem;
        }

        public void Initialize()
        {
            view.Initialize(localizationSystem);
            taskService.OnTasksUpdated += OnTasksUpdated;
            Setup(taskService.ActiveTasks[0]);
        }

        public void Cleanup()
        {
            view.Cleanup();
            if (currentTask != null)
            {
                currentTask.OnProgressChanged -= UpdateProgress;
            }
        }

        private void Setup(TasksContainer tasks)
        {
            if (currentTask != null)
            {
                currentTask.OnProgressChanged -= UpdateProgress;
            }

            currentTask = tasks;
            currentTask.OnProgressChanged += UpdateProgress;
            view.Setup(currentTask.Config);
        }

        private void UpdateProgress(float progress)
        {
            view.UpdateProgress(progress);
        }
        
        private void OnTasksUpdated(List<TasksContainer> tasksContainers)
        {
             Setup(tasksContainers[0]);
        }
    }
}