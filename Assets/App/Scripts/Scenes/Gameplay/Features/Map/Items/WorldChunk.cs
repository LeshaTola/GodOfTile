using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Map.Visualizators
{
    public class WorldChunk : MonoBehaviour
    {
        [SerializeField] private Canvas UI;

        private Vector2Int id;

        public void Initialize(Vector2Int id)
        {
            this.id = id;
        }

        public void ShowUI()
        {
            UI.gameObject.SetActive(true);
        }

        public void HideUI()
        {
            UI.gameObject.SetActive(true);
        }
    }
}