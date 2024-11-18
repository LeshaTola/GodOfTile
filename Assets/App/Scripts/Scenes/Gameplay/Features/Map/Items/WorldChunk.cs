using System;
using App.Scripts.Modules.Localization;
using App.Scripts.Modules.ObjectPool.PooledObjects;
using App.Scripts.Modules.ObjectPool.Pools;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Map.Items
{
    public class WorldChunk : MonoBehaviour, IPoolableObject<WorldChunk>
    {
        [SerializeField] private WorldChunkUI UI;

        private Action buyAction;
        private IPool<WorldChunk> pool;

        public void Initialize(Action action, ILocalizationSystem localizationSystem)
        {
            if (buyAction != null)
            {
                UI.OnBuyButtonClicked -= buyAction;
            }
            else
            {
                UI.Initialize(localizationSystem);
            }

            buyAction = action;

            UI.OnBuyButtonClicked += buyAction;
        }

        public void ShowUI()
        {
            UI.Show();
        }

        public void HideUI()
        {
            UI.Hide();
        }

        public void OnGet(IPool<WorldChunk> pool)
        {
            this.pool = pool;
        }

        public void Release()
        {
            if (pool != null)
            {
                pool.Release(this);
                return;
            }

            Destroy(gameObject);
        }

        public void OnRelease()
        {
            HideUI();
        }
    }
}