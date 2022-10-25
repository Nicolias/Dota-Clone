using UnityEngine;
using UnityEngine.AI;

namespace Characters.StateMashine
{
    public class MoveState : BaseState
    {
        private Vector3 _lastTargetPosition;
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
            _lastTargetPosition = Vector3.zero;
        }

        public override void MoveTo(Vector3 position)
        {
            if (position == _lastTargetPosition) return;

            _agent.SetDestination(position);            

            _lastTargetPosition = position;
        }
    }
}