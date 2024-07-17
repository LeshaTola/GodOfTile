using App.Scripts.Modules.Localization;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI
{
    public abstract class SystemUI : MonoBehaviour, ISystemUI
    {
        public abstract void Init(TileSystem system, ILocalizationSystem localizationSystem);
        public abstract void Translate();
        public abstract void Cleanup();

        public void Destroy()
        {
            Cleanup();
            Destroy(gameObject);
        }
    }
}