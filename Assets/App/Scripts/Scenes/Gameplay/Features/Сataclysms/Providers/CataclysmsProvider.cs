using System;
using System.Collections.Generic;
using System.Linq;
using App.Scripts.Modules.Saves;
using App.Scripts.Modules.StateMachine.Services.CleanupService;
using App.Scripts.Modules.StateMachine.Services.InitializeService;
using App.Scripts.Modules.StateMachine.Services.UpdateService;
using App.Scripts.Modules.TimeProvider;
using App.Scripts.Scenes.Gameplay.Features.Map.Providers.Grid;
using UnityEngine;
using Random = UnityEngine.Random;

namespace App.Scripts.Scenes.Gameplay.Features.Сataclysms.Providers
{
    public class CataclysmsProvider : IUpdatable, IInitializable, ICleanupable
    {
        public event Action<float> OnTimerChanged;
        public event Action<CataclysmConfig> OnCataclysmChanged;
        
        private readonly CataclysmFactory cataclysmFactory;
        private readonly IGridProvider gridProvider;
        private readonly ITimeProvider timeProvider;

        private CataclysmData cataclysmData;
        
        public float Timer { get; set; }

        public CataclysmsProviderConfig Config { get; }

        public CataclysmsProvider(CataclysmFactory cataclysmFactory,
            CataclysmsProviderConfig config,
            IGridProvider gridProvider,
            ITimeProvider timeProvider)
        {
            this.cataclysmFactory = cataclysmFactory;
            Config = config;
            this.gridProvider = gridProvider;
            this.timeProvider = timeProvider;
        }

        public void Initialize()
        {
            if (cataclysmData == null)
            {
                GetCataclysm();
            }
        }

        public void Update()
        {
            Timer -= timeProvider.DeltaTime;
            if (Timer <= 0)
            {
                ResetTimer();
                GetCataclysm();
                ApplyCataclism();
            }
            OnTimerChanged?.Invoke(Timer);
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
            var configCataclysm = Config.Cataclysms[Random.Range(0, Config.Cataclysms.Count)];
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
            Timer = Config.Cooldown;
        }
    }

    public class CataclysmData
    {
        public CataclysmConfig Cataclysm;
        public Vector2Int TargetPosition;
        
    }
}