using System.Collections.Generic;
using System.Linq;
using App.Scripts.Modules.PopupAndViews.Popups.Tutorial;
using App.Scripts.Modules.TasksSystem.CompleteActions;
using App.Scripts.Scenes.Gameplay.Features.Commands.Provider;
using App.Scripts.Scenes.Gameplay.Features.Time.Services.TimeServices;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.TasksSystem.CompleteActions
{
    public class ShowTutorialAction : CompleteAction
    {
        [SerializeField] private TutorialPopupData tutorialData;

        private readonly TutorialPopupRouter tutorialPopupRouter;
        private readonly ITimeService timeService;
        private readonly ICommandsProvider commandsProvider;

        public ShowTutorialAction(TutorialPopupRouter tutorialPopupRouter,
            ITimeService timeService,
            ICommandsProvider commandsProvider)
        {
            this.tutorialPopupRouter = tutorialPopupRouter;
            this.timeService = timeService;
            this.commandsProvider = commandsProvider;
        }

        public override void Execute()
        {
            var newTutorialData = new TutorialPopupData()
            {
                Header = tutorialData.Header,
                Tutorials = tutorialData.Tutorials.ToList(),
                Command = null
            };
            tutorialPopupRouter.ShowPopup(newTutorialData).Forget();
        }

        public override void Import(CompleteAction original)
        {
            var concrete = (ShowTutorialAction) original;
            tutorialData = concrete.tutorialData;
        }

        public override List<RewardData> GetRewardData()
        {
            return new List<RewardData>();
        }
    }
}