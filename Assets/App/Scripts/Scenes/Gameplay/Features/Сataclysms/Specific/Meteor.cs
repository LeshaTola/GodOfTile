using App.Scripts.Modules.CameraSwitchers;
using App.Scripts.Modules.TimeProvider;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Creation.Services.TilesCreation;
using App.Scripts.Scenes.Gameplay.Features.Tiles.General;
using App.Scripts.Scenes.Gameplay.Features.Сataclysms.Providers;
using DG.Tweening;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace App.Scripts.Scenes.Gameplay.Features.Сataclysms.Specific
{
    public class Meteor : Cataclysm
    {
        [SerializeField] private float _radius;
        [SerializeField] private float _speed;

        private Vector3 target;

        private ITimeProvider timeProvider;
        private ICameraSwitcher cameraSwitcher;
        private ITilesCreationService tilesCreationService;

        private bool isComplete = false;
        
        [Inject]
        public void Construct(ITimeProvider timeProvider,
            ICameraSwitcher cameraSwitcher,
            ITilesCreationService tilesCreationService)
        {
            this.timeProvider = timeProvider;
            this.cameraSwitcher = cameraSwitcher;
            this.tilesCreationService = tilesCreationService;
        }

        public override void Attack(Vector2Int position)
        {
            transform.position = GetSpawnPositionOffscreen();
            target = new Vector3(position.x, 0, position.y);
            transform.forward = target - transform.position;
        }

        public override void Update()
        {
            transform.position
                = Vector3.MoveTowards(
                    transform.position,
                    target,
                    _speed * timeProvider.DeltaTime);
                    
            if (isComplete)
            {
                return;
            }

            var animScaler = timeProvider.TimeMultiplier == 0? float.MinValue : timeProvider.TimeMultiplier; 
            if (!(Vector3.Distance(transform.position, target) <= 0.4f*animScaler))
            {
                return;
            }
            
            isComplete = true;
            transform
                .DOScale(0, 1f/animScaler)
                .SetEase(Ease.InBack)
                .onComplete += () => Destroy(gameObject);
            
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Tile tile))
            {
                tilesCreationService.DestroyTile(tile.Position);
            }
        }


        private Vector3 GetSpawnPositionOffscreen()
        {
            var cam = cameraSwitcher.CurrentCamera;
            Vector3 screenCenter = cam.transform.position;

            float camHeight = cam.m_Lens.OrthographicSize * 2;
            float camWidth = camHeight * cam.m_Lens.Aspect;

            // Выбираем случайную сторону за пределами экрана
            int side = Random.Range(0, 4);
            float x, y;

            switch (side)
            {
                case 0: // Слева
                    x = screenCenter.x - camWidth / 2 - 2;
                    y = Random.Range(screenCenter.y - camHeight / 2, screenCenter.y + camHeight / 2);
                    break;
                case 1: // Справа
                    x = screenCenter.x + camWidth / 2 + 2;
                    y = Random.Range(screenCenter.y - camHeight / 2, screenCenter.y + camHeight / 2);
                    break;
                case 2: // Сверху
                    x = Random.Range(screenCenter.x - camWidth / 2, screenCenter.x + camWidth / 2);
                    y = screenCenter.y + camHeight / 2 + 2;
                    break;
                default: // Снизу
                    x = Random.Range(screenCenter.x - camWidth / 2, screenCenter.x + camWidth / 2);
                    y = screenCenter.y - camHeight / 2 - 2;
                    break;
            }

            return new Vector3(x, y, 0);
        }
    }
}