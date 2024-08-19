using System;
using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.Effects.Specific.ChangeResourceEarningEffect.UI.Providers;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.ValidationStrategies;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.ResourceEarners;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.Effects.Specific.ChangeResourceEarningEffect
{
    public class ChangeResourceEarningEffect : IEffect
    {
        [SerializeField] private float earningAmountMultiplier;
        [SerializeField] private ChangeResourceEarningEffectorUIProvider systemUIProvider;

        private Effector effector;
        private IValidationStrategy validationStrategy;

        public IValidationStrategy ValidationStrategy => validationStrategy;
        public Effector Effector => effector;
        public float EarningAmountMultiplier => earningAmountMultiplier;
        public ISystemUIProvider SystemUIProvider => systemUIProvider;
        
        public void Initialize(Effector effector)
        {
            validationStrategy = new WhiteListValidationStrategy(new List<Type>()
            {
                typeof(ResourceEarner) 
            });
            this.effector = effector;
        }

        public void AddEffect(TileSystemData tileSystemData)
        {
            ((ResourceEarnerSystemData) tileSystemData).AmountPerSecond *= EarningAmountMultiplier;
        }

        public void RemoveEffect(TileSystemData tileSystemData)
        {
            ((ResourceEarnerSystemData) tileSystemData).AmountPerSecond /= EarningAmountMultiplier;
        }
    }
}