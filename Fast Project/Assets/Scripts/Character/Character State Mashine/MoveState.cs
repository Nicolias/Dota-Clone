using UnityEngine;
using UnityEngine.AI;

namespace Characters.StateMashine
{
    public class MoveState : BaseState
    {
        private readonly NavMeshAgent _agent;

        public MoveState(CharacterViwe characterViwe, IStationStateSwitcher stateSwitcher) : base(stateSwitcher)
        {
            _agent = characterViwe.Agent;
        }

        public override void Attack(ITarget target)
        {
            StateSwitcher.SwitchState<AttackState>();
            Character.Attack(target);
        }

        public override void EnterState()
        {
            
        }

        public override void ExitState()
        {
            _agent.ResetPath();
        }

        public override void MoveTo(Vector3 position)
        {
            _agent.SetDestination(position);
        }
    }
}