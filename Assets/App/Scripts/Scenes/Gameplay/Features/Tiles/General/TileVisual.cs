using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Animations;
using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using Cysharp.Threading.Tasks;
using TileSystem;
using UnityEngine;
using Zenject;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Tiles
{
    public class TileVisual : MonoBehaviour
    {
        [SerializeField]
        private TileVisualConfig config;

        [SerializeField]
        private Transform buildingPosition;

        [Header("Renderer")]
        [SerializeField]
        private MeshRenderer meshRenderer;

        private ITileAnimator animator;

        private Material defaultMaterial;

        [Inject]
        public void Construct(ITileAnimator animator)
        {
            this.animator = animator;
            animator.Setup(this);
        }

        public void Initialize(Vector2Int size, Material material, GameObject building)
        {
            Resize(size);
            defaultMaterial = material;
            if (building != null)
            {
                Instantiate(building, buildingPosition);
            }
        }

        public async UniTask PlayActive()
        {
            await animator.PlayActiveAnimation();
        }

        public async UniTask PlayCreation()
        {
            await animator.PlayCreationAnimation();
        }

        public async UniTask PlayDestroying()
        {
            await animator.PlayActiveAnimation();
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
    }
}
