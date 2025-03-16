using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using App.Scripts.Features.UI.PairedTexts;
using App.Scripts.Modules.Localization;
using App.Scripts.Modules.Localization.Localizers;
using App.Scripts.Modules.Tasks.CompleteActions;
using App.Scripts.Modules.Tasks.Configs;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Scenes.Gameplay.Features.TasksSystem.View
{
    public class TaskView : MonoBehaviour
    {
        [SerializeField] private TMPLocalizer headerText;
        [SerializeField] private PairedText nameText;
        [SerializeField] private PairedText descriptionText;

        [SerializeField] private PairedText progressText;
        [SerializeField] private Slider progressSlider;

        [SerializeField] private int maxRewards = 3;
        [SerializeField] private TMPLocalizer rewardsText;
        [SerializeField] private RewardElement rewardElementPrefab;
        [SerializeField] private Transform rewardElementContainer;
        
        public void Initialize(ILocalizationSystem localizationSystem)
        {
            headerText.Initialize(localizationSystem);
            
            nameText.Initialize(localizationSystem);
            descriptionText.Initialize(localizationSystem);
            progressText.Initialize(localizationSystem);
            
            rewardsText.Initialize(localizationSystem);
        }

        public void Cleanup()
        {
            headerText.Cleanup();
            
            nameText.Cleanup();
            descriptionText.Cleanup();
            progressText.Cleanup();
            
            rewardsText.Cleanup();
        }

        public void Setup(TaskConfig config)
        {
            SetupRewards(config);
            SetupTexts(config);
            UpdateProgress(0);
        }

        private void SetupRewards(TaskConfig config)
        {
            Default();
            var rewards = GetRewardsData(config);
            foreach (var reward in rewards)
            {
                var element = Instantiate(rewardElementPrefab, rewardElementContainer);
                element.Setup(reward);
            }
        }

        public void UpdateProgress(float progress)
        {
            progress = Mathf.Clamp01(progress);
            progressText.Value.Text.text = $"{Mathf.RoundToInt(progress* 100)}%";
            progressSlider.value =progress;
        }

        private  List<RewardData> GetRewardsData(TaskConfig config)
        {
            return config.CompleteActions
                .SelectMany(x => x.GetRewardData())
                .Take(maxRewards) 
                .ToList();
        }

        private void SetupTexts(TaskConfig config)
        {
            nameText.Value.Key = config.Name;
            descriptionText.Value.Key = config.Description;
            Translate();
        }

        private void Default()
        {
            foreach (Transform child in rewardElementContainer)
            {
                Destroy(child.gameObject);
            }
        }

        private void Translate()
        {
            headerText.Translate();
            
            nameText.Translate();
            descriptionText.Translate();
            progressText.Translate();
            
            rewardsText.Translate();
        }
    }
}