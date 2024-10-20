using System;
using App.Scripts.Scenes.Gameplay.Features.Tiles.General;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI;
using UnityEngine;

namespace App.Scripts.Scenes.MainMenu.Tiles.Systems.ExitSystem
{
    public class ExitSystemData : TileSystemData
    {
        [SerializeField] private ExitSystemUIProvider systemUIProvider;
        
        [field:SerializeField] public string HeaderKey { get; private set; }
        
        public override ISystemUIProvider SystemUIProvider => systemUIProvider;
    }
    
    public class ExitSystem:TileSystem
    {
        [SerializeField] private ExitSystemData data;
        
        public ExitSystem(Tile parentTile) : base(parentTile)
        {
            
        }

        public override TileSystemData Data => data;
    }
}