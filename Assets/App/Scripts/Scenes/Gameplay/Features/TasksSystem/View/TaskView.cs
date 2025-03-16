using System.Globalization;
using App.Scripts.Features.UI.PairedTexts;
using App.Scripts.Modules.Localization;
using App.Scripts.Modules.Localization.Localizers;
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
            Default();
            //SetupRewards
            
            SetupTexts(config);
            UpdateProgress(0);
        }

        public void UpdateProgress(float progress)
        {
            progress = Mathf.Clamp01(progress);
            progressText.Value.Text.text = $"{Mathf.RoundToInt(progress* 100)}%";
            progressSlider.value =progress;
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