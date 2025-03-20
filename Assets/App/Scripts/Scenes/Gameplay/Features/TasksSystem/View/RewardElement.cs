using App.Scripts.Modules.Tasks.CompleteActions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace App.Scripts.Scenes.Gameplay.Features.TasksSystem.View
{
    public class RewardElement : MonoBehaviour
    {
        [SerializeField] private Image rewardImage;
        [SerializeField] private TextMeshProUGUI rewardText;

        public void Setup(RewardData reward)
        {
            Setup(reward.Sprite, reward.Text);
        }

        public void Setup(Sprite rewardSprite, string count)
        {
            rewardImage.sprite = rewardSprite;
            rewardText.text = count;
        }
    }
}