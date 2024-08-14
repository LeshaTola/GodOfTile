using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Map.WaterMaterialController.Configs
{
    [CreateAssetMenu(fileName = "WaterMaterialConfig", menuName = "Configs/Materials/Water")]
    public class WaterMaterialConfig : ScriptableObject
    {
        [field: SerializeField] public Material Material { get; private set; }
        [field: SerializeField] public Vector4 StartSpeed { get; private set; }
    }
}