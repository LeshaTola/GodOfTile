using System.Collections.Generic;

namespace App.Scripts.Modules.Tasks.CompleteActions
{
    public abstract class CompleteAction
    {
        public abstract void Execute();
        public abstract void Import(CompleteAction original);
        public abstract List<RewardData> GetRewardData();
    }
}