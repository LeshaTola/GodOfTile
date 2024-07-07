using Sirenix.OdinInspector;
using UnityEngine;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Inventory.Configs
{
    [CreateAssetMenu(fileName = "ResourceConfig", menuName = "Configs/Resource")]
    public class ResourceConfig : ScriptableObject
    {
        [PreviewField]
        [SerializeField]
        private Sprite sprite;

        [SerializeField]
        private string resourceName;

        [Min(0)]
        [SerializeField]
        private int startAmount;

        public Sprite Sprite
        {
            get => sprite;
        }
        public string ResourceName
        {
            get => resourceName;
        }
        public int StartAmount
        {
            get => startAmount;
        }
    }
}
