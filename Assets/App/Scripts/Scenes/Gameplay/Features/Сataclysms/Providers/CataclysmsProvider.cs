using System;
using System.Collections.Generic;
using System.Linq;
using App.Scripts.Modules.StateMachine.Services.CleanupService;
using App.Scripts.Modules.StateMachine.Services.UpdateService;
using App.Scripts.Modules.TimeProvider;
using App.Scripts.Scenes.Gameplay.Features.Map.Providers.Grid;
using UnityEngine;
using IInitializable = App.Scripts.Modules.StateMachine.Services.InitializeService.IInitializable;
using Random = UnityEngine.Random;

namespace App.Scripts.Scenes.Gameplay.Features.Сataclysms.Providers
{
    public class CataclysmsProvider : IUpdatable, IInitializable, ICleanupable
    {
        public event Action<float> OnTimerChanged;
        public event Action<CataclysmConfig> OnCataclysmChanged;
        
        private readonly CataclysmFactory cataclysmFactory;
        private readonly CataclysmsProviderConfig config;
        private readonly IGridProvider gridProvider;
        private readonly ITimeProvider timeProvider;

        private CataclysmData cataclysmData;
        private float timer;

        public CataclysmsProvider(CataclysmFactory cataclysmFactory, CataclysmsProviderConfig config,
            IGridProvider gridProvider, ITimeProvider timeProvider)
        {
            this.cataclysmFactory = cataclysmFactory;
            this.config = config;
            this.gridProvider = gridProvider;
            this.timeProvider = timeProvider;
        }

        public void Initialize()
        {
            if (cataclysmData == null)
            {
                GetCataclysm();
            }
            ResetTimer();
        }

        public void Update()
        {
            timer -= timeProvider.DeltaTime;
            if (timer <= 0)
            {
                ResetTimer();
                GetCataclysm();
                ApplyCataclism();
            }
            OnTimerChanged?.Invoke(timer);
        }

        public void Cleanup()
        {
        }

        private void ApplyCataclism()
        {
            if (cataclysmData.TargetPosition == default)
            {
                return;
            }
            var catoclism = cataclysmFactory.Get(cataclysmData.Cataclysm);
            catoclism.Attack(cataclysmData.TargetPosition);
        }

        private void GetCataclysm()
        {
            var configCataclysm = config.Cataclysms[Random.Range(0, config.Cataclysms.Count)];
            OnCataclysmChanged?.Invoke(configCataclysm);
            cataclysmData = new()
            {
                Cataclysm = configCataclysm,
                TargetPosition = GetTargetTilePosition()
            };
        }

        private Vector2Int GetTargetTilePosition()
        {
            List<Vector2Int> nonNullIndices = GetTilePositions();
            return nonNullIndices.Count > 0 ? nonNullIndices[Random.Range(0, nonNullIndices.Count)] : default;
        }

        private List<Vector2Int> GetTilePositions()
        {
            return Enumerable.Range(0, gridProvider.Grid.GetLength(0))
                .SelectMany(x => Enumerable.Range(0, gridProvider.Grid.GetLength(1))
                    .Where(y => gridProvider.Grid[x, y] != null)
                    .Select(y => new Vector2Int(x, y)))
                .ToList();
        }

        private void ResetTimer()
        {
            timer = config.Cooldown;
        }
    }

    public class CataclysmData
    {
        public CataclysmConfig Cataclysm;
        public Vector2Int TargetPosition;
        
    }
}