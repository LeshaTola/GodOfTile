using System;
using System.Collections.Generic;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.Effects.Specific.ChangeResourceEarningEffect.UI.
    Providers;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.ValidationStrategies;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.ResourceEarners;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Effectors.Effects.Specific.ChangeResourceEarningEffect
{
    public class CollectTilesAroundEffect : IEffect
    {
        [SerializeField] private float earningAmountPerValidTile;
        //[SerializeField] private ChangeResourceEarningEffectorUIProvider systemUIProvider;
        [SerializeField] private List<TileConfig> whiteList;

        private IResourceEarnerService resourceEarnerService;

        private Effector effector;
        private IValidationStrategy validationStrategy;

        public IValidationStrategy ValidationStrategy => validationStrategy;
        public Effector Effector => effector;
        public float EarningAmountMultiplier => earningAmountPerValidTile;
        public ISystemUIProvider SystemUIProvider => null; //systemUIProvider;

        public void Initialize(Effector effector)
        {
            //validationStrategy = new OwnerValidationStrategy(effector.ParentTile);
            validationStrategy = new TilesValidationStrategy(whiteList);
            this.effector = effector;
        }

        public void AddEffect(TileSystemData tileSystemData)
        {
            ((ResourceEarnerSystemData)effector.ParentTile.Config.
                GetSystem<ResourceEarner>().Data).AmountPerSecond += earningAmountPerValidTile;
        }

        public void RemoveEffect(TileSystemData tileSystemData)
        {
            ((ResourceEarnerSystemData)effector.ParentTile.Config.
                GetSystem<ResourceEarner>().Data).AmountPerSecond -= earningAmountPerValidTile;
        }
    }
}