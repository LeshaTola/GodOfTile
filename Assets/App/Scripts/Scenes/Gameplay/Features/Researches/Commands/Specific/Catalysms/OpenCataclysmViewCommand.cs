using App.Scripts.Scenes.Gameplay.Features.Сataclysms.UI;
using UnityEngine;

namespace App.Scripts.Scenes.Gameplay.Features.Researches.Commands.Specific.Catalysms
{
    public class OpenCataclysmViewCommand : ResearchCommand
    {
        [SerializeField] private ResearchCommandData data;

        public override ResearchCommandData Data => data;
        private readonly CataclysmViewPresenter presenter;

        public OpenCataclysmViewCommand(ResearchCommandData data, CataclysmViewPresenter presenter)
        {
            this.data = data;
            this.presenter = presenter;
        }

        public override void Execute()
        {
            presenter.Show();
        }
    }
}