namespace Assets.App.Scripts.Scenes.Gameplay.Features.Commands.General
{
    public abstract class LabeledCommand : ILabeledCommand
    {
        public string Label { get; private set; }

        public LabeledCommand(string label)
        {
            Label = label;
        }

        public abstract void Execute();
    }
}
