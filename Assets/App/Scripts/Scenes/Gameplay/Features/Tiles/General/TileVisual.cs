using App.Scripts.Scenes.Gameplay.Features.Tiles.Animations;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.General
{
    public class TileVisual : MonoBehaviour
    {
        [SerializeField] private TileVisualConfig config;
        [SerializeField] private GameObject glow;
        [SerializeField] private Transform buildingPosition;

        [Header("Renderer")]
        [SerializeField] private MeshRenderer meshRenderer;

        private ITileAnimator animator;

        private Material defaultMaterial;

        [Inject]
        public void Construct(ITileAnimator animator)
        {
            this.animator = animator;
        }

        public void Initialize(Vector2Int size, Material material, GameObject building)
        {
            defaultMaterial = material;

            DestroyBuilding();
            Resize(size);
            SetState(TileState.Default);

            if (building != null)
            {
                Instantiate(building, buildingPosition);
            }
        }

        public void StartGlow()
        {
            glow.SetActive(true);
        }
        
        public void StopGlow()
        {
            glow.SetActive(false);
        }

        public async UniTask PlayRotation()
        {
            await animator.PlayRotationAnimation(this);
        }

        public async UniTask PlayActive()
        {
            await animator.PlayActiveAnimation(this);
        }

        public async UniTask PlayCreation()
        {
            await animator.PlayCreationAnimation(this);
        }

        public async UniTask PlayDestroying()
        {
            await animator.PlayActiveAnimation(this);
        }

        public void SetState(TileState state)
        {
            switch (state)
            {
                case TileState.Default:
                    meshRenderer.material = defaultMaterial;
                    break;
                case TileState.Wrong:
                    meshRenderer.material = config.WrongMaterial;
                    break;
                case TileState.Correct:
                    meshRenderer.material = config.CorrectMaterial;
                    break;
            }
        }

        private void Resize(Vector2Int size)
        {
            transform.localScale = new Vector3(size.x, transform.localScale.y, size.y);
            transform.localPosition = new Vector3(
                (size.x - 1) / 2f,
                transform.position.y,
                (size.y - 1) / 2f
            );
        }

        private void OnDestroy()
        {
            animator.Cleanup();
        }

        private void DestroyBuilding()
        {
            foreach (Transform child in buildingPosition)
            {
                Destroy(child.gameObject);
            }
        }
    }
}