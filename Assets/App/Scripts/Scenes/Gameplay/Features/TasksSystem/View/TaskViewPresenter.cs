using System.Collections.Generic;
using App.Scripts.Modules.Localization;
using App.Scripts.Modules.StateMachine.Services.CleanupService;
using App.Scripts.Modules.StateMachine.Services.InitializeService;
using App.Scripts.Modules.Tasks.Providers;
using App.Scripts.Modules.Tasks.Tasks;

namespace App.Scripts.Scenes.Gameplay.Features.TasksSystem.View
{
    public class TaskViewPresenter : IInitializable, ICleanupable
    {
        private readonly TaskView view;
        private readonly TasksProvider tasksProvider;
        private readonly ILocalizationSystem localizationSystem;

        private TasksContainer currentTask;

        public TaskViewPresenter(TaskView view,
            TasksProvider tasksProvider, 
            ILocalizationSystem localizationSystem)
        {
            this.view = view;
            this.tasksProvider = tasksProvider;
            this.localizationSystem = localizationSystem;
        }

        public void Initialize()
        {
            view.Initialize(localizationSystem);
            tasksProvider.OnTasksUpdated += OnTasksUpdated;
            // Setup(tasksProvider.ActiveTasks[0]);
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

        private void OnTasksUpdated(List<TasksContainer> tasks)
        {
            Setup(tasks[0]);
        }
    }
}