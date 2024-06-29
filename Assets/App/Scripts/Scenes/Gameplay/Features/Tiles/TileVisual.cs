using Assets.App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using TileSystem;
using UnityEngine;

namespace Assets.App.Scripts.Scenes.Gameplay.Features.Tiles
{
    public class TileVisual : MonoBehaviour
    {
        [SerializeField]
        private TileVisualConfig config;

        [Header("Renderer")]
        [SerializeField]
        private MeshRenderer meshRenderer;

        private Material defaultMaterial;

        Vector2Int size;

        public void Initialize(Vector2Int size, Material material)
        {
            this.size = size;
            transform.localScale = new Vector3(size.x, transform.localScale.y, size.y);
            transform.localPosition = new Vector3(
                (size.x - 1) / 2f,
                transform.position.y,
                (size.y - 1) / 2f
            );
            defaultMaterial = material;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Vector3 newPos =
                transform.position
                + new Vector3(-(size.x - 1) / 2f, transform.position.y, -(size.y - 1) / 2f);

            Gizmos.DrawWireSphere(newPos, 0.1f);

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, 0.1f);
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
    }
}
