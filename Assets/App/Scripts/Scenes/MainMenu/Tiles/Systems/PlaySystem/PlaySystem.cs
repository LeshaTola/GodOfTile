using App.Scripts.Scenes.Gameplay.Features.Tiles.General;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI;
using UnityEngine;

namespace App.Scripts.Scenes.MainMenu.Tiles.Systems.PlaySystem
{
    public class PlaySystemData : TileSystemData
    {
        [SerializeField] private PlaySystemUIProvider systemUIProvider;
        
        [field:SerializeField] public string HeaderKey { get; private set; }
        
        public override ISystemUIProvider SystemUIProvider => systemUIProvider;
    }
    
    public class PlaySystem:TileSystem
    {
        [SerializeField] private PlaySystemData data;
        
        public PlaySystem(Tile parentTile, PlaySystemData data) : base(parentTile)
        {
            this.data = data;
        }

        public override TileSystemData Data => data;
    }
}