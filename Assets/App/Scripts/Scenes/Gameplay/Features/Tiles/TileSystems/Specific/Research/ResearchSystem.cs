using App.Scripts.Scenes.Gameplay.Features.Researches.Services;
using App.Scripts.Scenes.Gameplay.Features.Tiles.General;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.Research.UI.Providers;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.ResourceEarners;
using App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.UI;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Tiles.TileSystems.Specific.Research
{
    public class ResearchSystemData : TileSystemData
    {
        [SerializeField] private ResearchSystemUIProvider uiProvider;

        [field: TextArea]
        [field: SerializeField] public string Description { get; private set; }

        public override ISystemUIProvider SystemUIProvider => uiProvider;
    }

    public class ResearchSystem : TileSystem
    {
        [SerializeField] private ResearchSystemData data;

        private IResearchService researchService;

        public override TileSystemData Data => data;
        
        public ResearchSystem(Tile parentTile, ResearchSystemData data, IResearchService researchService) :
            base(parentTile)
        {
            this.data = data;
            this.researchService = researchService;
        }

        public override void Start()
        {
            base.Start();
            researchService.AddResearchSystem(this);
        }

        public override void Stop()
        {
            base.Stop();
            researchService.RemoveResearchSystem(this);
        }
    }
}