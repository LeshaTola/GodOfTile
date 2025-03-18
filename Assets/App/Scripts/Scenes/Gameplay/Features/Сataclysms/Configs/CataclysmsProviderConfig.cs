using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Сataclysms.Providers
{
    [CreateAssetMenu(menuName = "Configs/Cataclysms/Provider", fileName = "CataclysmsProviderConfig")]
    public class CataclysmsProviderConfig : ScriptableObject
    {
        [field:SerializeField] public int Cooldown {get; private set;}
        [field:SerializeField] public List<CataclysmConfig> Cataclysms {get; private set;}
    }
}