using System;
using App.Scripts.Scenes.Gameplay.Features.Tiles.General;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.Effects;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.ValidationStrategies;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.Defence
{
    public class DefenceEffect : IEffect
    {
        [SerializeField] private DefenceEffectUIProvider systemUIProvider;
        [SerializeField] private DefenceShieldArea shieldArea;
        
        [field: SerializeField] public string Description { get; private set; }

        private Effector effector;
        private IValidationStrategy validationStrategy;
        private DefenceShieldArea defenceShieldArea;

        public IValidationStrategy ValidationStrategy => validationStrategy;
        public Effector Effector => effector;
        public ISystemUIProvider SystemUIProvider => systemUIProvider;
        
        public void Initialize(Effector effector)
        {
            validationStrategy = new NoValidationStrategy();
            this.effector = effector;
        }

        public void AddEffect(TileSystem tileSystemData)
        {
            tileSystemData.ParentTile.SetDefence(true);
        }

        public void RemoveEffect(TileSystem tileSystemData)
        {
            
            tileSystemData.ParentTile.SetDefence(false);
        }
    }
}