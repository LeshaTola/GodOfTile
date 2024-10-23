using App.Scripts.Scenes.Gameplay.Features.Tiles.General;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI;
using UnityEngine;

namespace App.Scripts.Scenes.MainMenu.Tiles.Systems.SettingsSystem
{
    public class SettingsSystemData : TileSystemData
    {
        [SerializeField] private SettingsSystemUIProvider systemUIProvider;
        public override ISystemUIProvider SystemUIProvider => systemUIProvider;
    }
    
    public class SettingsSystem:TileSystem
    {
        [SerializeField] private SettingsSystemData data;
        
        public SettingsSystem(Tile parentTile, SettingsSystemData data) : base(parentTile)
        {
            this.data = data;
        }

        public override TileSystemData Data => data;
    }
}