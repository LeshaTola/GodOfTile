using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI
{
    public abstract class SystemUI : MonoBehaviour
    {
        public abstract void Show();
        public abstract void Translate();
        public abstract void Cleanup();

        public void Destroy()
        {
            Cleanup();
            Destroy(gameObject);
        }
    }
}