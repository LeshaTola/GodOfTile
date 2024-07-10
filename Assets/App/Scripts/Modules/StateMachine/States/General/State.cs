using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Features.StateMachineCore.States.General;
using Modules.StateMachineCore;

namespace Features.StateMachineCore.States
{
    public abstract class State
    {
        protected StateMachine StateMachine;
        protected List<IStateStep> StateSteps;

        protected State(string id)
        {
            Id = id;
        }

        public void Initialize(StateMachine stateMachine)
        {
            StateMachine = stateMachine;
            StateSteps = new();
        }

        public string Id { get; private set; }

        public virtual async UniTask Enter()
        {
            foreach (var step in StateSteps)
            {
                await step.Enter();
            }
        }

        public virtual async UniTask Exit()
        {
            foreach (var step in StateSteps)
            {
                await step.Exit();
            }
        }

        public virtual async UniTask Update()
        {
            foreach (var step in StateSteps)
            {
                await step.Update();
            }
        }

        public void AddStep(IStateStep step)
        {
            StateSteps.Add(step);
            step.Init(this, StateMachine);
        }

        public void AddSteps(IEnumerable<IStateStep> steps)
        {
            foreach (var step in steps)
            {
                AddStep(step);
            }
        }

        public void RemoveStep(IStateStep step)
        {
            StateSteps.Remove(step);
        }
    }
}
