using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Providers.Collection;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Researches.Commands.Spicific.OpenTileCommand
{
    public class OpenTileCommand: ResearchCommand
    {
        [SerializeField] private TileConfig tileConfig;

        private ITileCollectionProvider tileCollectionProvider;
        
        public OpenTileCommand(ITileCollectionProvider tileCollectionProvider)
        {
            this.tileCollectionProvider = tileCollectionProvider;
        }
        
        public override void Execute()
        {
            tileCollectionProvider.AddIfNotContains(tileConfig);
        }
    }
}