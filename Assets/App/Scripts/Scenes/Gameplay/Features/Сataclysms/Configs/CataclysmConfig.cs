using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Сataclysms.Providers
{
    [CreateAssetMenu(menuName = "Configs/Cataclysms/Cataclysm", fileName = "CataclysmConfig")]
    public class CataclysmConfig : ScriptableObject
    {
        [field:SerializeField] public Sprite Sprite {get; private set;}
        [field:SerializeField] public Cataclysm Prefab {get; private set;}
    }
}