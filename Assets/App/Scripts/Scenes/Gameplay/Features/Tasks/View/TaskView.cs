using App.Scripts.Features.UI.PairedTexts;
using App.Scripts.Modules.Localization;
using App.Scripts.Modules.Localization.Localizers;
using App.Scripts.Modules.Tasks.Configs;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Scenes.Gameplay.Features.Tasks.View
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
            UpdateProgress(0);
            //SetupRewards
            
            SetupTexts(config);
        }

        public void UpdateProgress(float progress)
        {
            progressSlider.value = Mathf.Clamp01(progress);
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