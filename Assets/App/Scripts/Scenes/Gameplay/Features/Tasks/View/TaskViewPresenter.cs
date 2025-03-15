using System.Collections.Generic;
using App.Scripts.Modules.StateMachine.Services.CleanupService;
using App.Scripts.Modules.StateMachine.Services.InitializeService;
using App.Scripts.Modules.Tasks.Providers;
using App.Scripts.Modules.Tasks.Tasks;

namespace App.Scripts.Scenes.Gameplay.Features.Tasks.View
{
    public class TaskViewPresenter : IInitializable, ICleanupable
    {
        private readonly TaskView view;
        private readonly TasksProvider tasksProvider;

        private TasksContainer currentTask;

        public TaskViewPresenter(TaskView view, TasksProvider tasksProvider)
        {
            this.view = view;
            this.tasksProvider = tasksProvider;
        }

        public void Initialize()
        {
            tasksProvider.OnTasksUpdated += OnTasksUpdated;
        }

        private void OnTasksUpdated(List<TasksContainer> tasks)
        {
            Setup(tasks[0]);
        }

        public void Cleanup()
        {
            if (currentTask != null)
            {
                currentTask.OnProgressChanged -= UpdateProgress;
            }
        }

        public void Setup(TasksContainer tasks)
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
    }
}