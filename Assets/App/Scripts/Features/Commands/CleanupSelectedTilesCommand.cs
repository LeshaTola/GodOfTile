using App.Scripts.Scenes.Gameplay.Features.Commands.General;
using App.Scripts.Scenes.Gameplay.Features.Tiles.Providers.Selection;

namespace App.Scripts.Features.Commands
{
    public class CleanupSelectedTilesCommand:LabeledCommand
    {
        private readonly ITileSelectionProvider tileSelectionProvider;

        public CleanupSelectedTilesCommand(string label, ITileSelectionProvider tileSelectionProvider) : base(label)
        {
            this.tileSelectionProvider = tileSelectionProvider;
        }

        public override void Execute()
        {
            tileSelectionProvider.Cleanup();
        }
    }
}