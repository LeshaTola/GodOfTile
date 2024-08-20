using App.Scripts.Scenes.Gameplay.Features.Tiles.Configs;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Providers.Collection;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Researches.Commands.Specific.OpenTileCommand
{
    public class OpenTileCommandData : ResearchCommandData
    {
        [field: SerializeField] public TileConfig TileConfig { get; private set; }
    }

    public class OpenTileCommand : ResearchCommand
    {
        [SerializeField] private OpenTileCommandData data;

        private ITileCollectionProvider tileCollectionProvider;

        public OpenTileCommand(OpenTileCommandData data, ITileCollectionProvider tileCollectionProvider)
        {
            this.data = data;
            this.tileCollectionProvider = tileCollectionProvider;
        }

        public override ResearchCommandData Data => data;

        public override void Execute()
        {
            tileCollectionProvider.AddIfNotContains(data.TileConfig);
        }
    }
}