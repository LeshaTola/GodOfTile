using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Time.Configs
{
    [CreateAssetMenu(fileName = "TimeSpeedConfig", menuName = "Configs/Time/Speed")]
    public class TimeSpeedConfig : ScriptableObject
    {
        [field: SerializeField, Min(0)] public float Speed1 { get; private set; }
        [field: SerializeField, Min(0)] public float Speed2 { get; private set; }
        [field: SerializeField, Min(0)] public float Speed3 { get; private set; }

    }
}