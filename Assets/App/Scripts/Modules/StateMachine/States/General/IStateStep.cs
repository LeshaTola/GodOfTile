using Cysharp.Threading.Tasks;
using Modules.StateMachineCore;

namespace Features.StateMachineCore.States.General
{
    public interface IStateStep
    {
        public bool IsComplete { get; }

        public void Init(State parentState, StateMachine stateMachine);
        public UniTask Enter();
        public UniTask Exit();
        public UniTask Update();
    }
}
