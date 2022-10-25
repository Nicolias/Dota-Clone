

using UnityEngine;
using UnityEngine.AI;

namespace Characters.StateMashine
{
    public class DeadState : BaseState
    {
        private readonly NavMeshAgent _agent;

        public DeadState(CharacterViwe characterViwe, IStationStateSwitcher stateSwitcher) : base(stateSwitcher)
        {
            _agent = characterViwe.Agent;
        }

        public override void Attack(ITarget target)
        {
        }

        public override void EnterState()
        {
            _agent.ResetPath();            
        }

        public override void ExitState()
        {
            
        }

        public override void MoveTo(Vector3 position)
        {
        }
    }
}