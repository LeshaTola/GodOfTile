using App.Scripts.Modules.TasksSystem.Tasks;
using App.Scripts.Scenes.Gameplay.Features.Сataclysms.Providers;

namespace App.Scripts.Scenes.Gameplay.Features.TasksSystem.Tasks.Tutorial
{
    public class WaitCataclysmTask : Task
    {
        private readonly CataclysmsProvider cataclysmsProvider;

        public WaitCataclysmTask(CataclysmsProvider cataclysmsProvider)
        {
            this.cataclysmsProvider = cataclysmsProvider;
        }

        public override void Start()
        {
            cataclysmsProvider.OnTimerChanged += UpdateProgress;
        }

        public override void Complete()
        {
            base.Complete();
            cataclysmsProvider.OnTimerChanged -= UpdateProgress;
        }

        public override ProgressPair GetProgress()
        {
            return new()
            {
                Progress = 0,
                Target = 0
            };
        }

        public override void SetProgress(ProgressPair progress)
        {
            
        }

        private void UpdateProgress(float remainigTime)
        {
            Progress = 1 - (remainigTime / cataclysmsProvider.Config.Cooldown);
        }
    }
}