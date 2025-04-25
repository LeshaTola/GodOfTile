using System;
using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.Effects.Specific.ChangeResourceEarningEffect.UI.
    Providers;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.ValidationStrategies;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.ResourceEarners;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI;
using Sirenix.OdinInspector;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.Effects.Specific.ChangeResourceEarningEffect
{
    public class ChangeResourceEarningEffect : IEffect
    {
        [SerializeField] private float earningAmountMultiplier;
        [SerializeField] private ChangeResourceEarningEffectorUIProvider systemUIProvider;
        [SerializeField] private bool isWiteList = false;
        [SerializeField, ShowIf(@"isWiteList")] private List<TileConfig> whiteList;

        private Effector effector;
        private IValidationStrategy validationStrategy;

        public IValidationStrategy ValidationStrategy => validationStrategy;
        public Effector Effector => effector;
        public float EarningAmountMultiplier => earningAmountMultiplier;
        public ISystemUIProvider SystemUIProvider => systemUIProvider;

        public void Initialize(Effector effector)
        {
            if (isWiteList)
            {
                validationStrategy = new TilesValidationStrategy(whiteList);
            }
            else
            {
                validationStrategy = new SystemsValidationStrategy(new List<Type>()
                {
                    typeof(ResourceEarner)
                });
            }
            
            this.effector = effector;
        }

        public void AddEffect(TileSystem tileSystemData)
        {
            ((ResourceEarnerSystemData) tileSystemData.Data).AmountPerSecond *= EarningAmountMultiplier;
        }

        public void RemoveEffect(TileSystem tileSystemData)
        {
            ((ResourceEarnerSystemData) tileSystemData.Data).AmountPerSecond /= EarningAmountMultiplier;
        }
    }
}