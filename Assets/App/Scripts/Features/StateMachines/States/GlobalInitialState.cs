using Cysharp.Threading.Tasks;
using Features.StateMachineCore.States;
using Modules.StateMachineCore;

namespace Assets.App.Scripts.StateMachines.States
{
    public class GlobalInitialState : State
    {
        public string NextStateId { get; set; }

        private static bool isValid = true;

        public GlobalInitialState(string id)
            : base(id) { }

        public override async UniTask Enter()
        {
            if (!isValid)
            {
                return;
            }

            await base.Enter();
            await StateMachine.ChangeState(NextStateId);

            isValid = false;
        }
    }
}
