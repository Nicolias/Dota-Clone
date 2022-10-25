

using UnityEngine;

namespace Characters.StateMashine
{
    public class IdleState : BaseState
    {
        private Base _enemyBase;

        public IdleState(IStationStateSwitcher stateSwitcher) : base(stateSwitcher)
        {
        }

        public IdleState(IStationStateSwitcher stateSwitcher, Base enemyBase) : base(stateSwitcher)
        {
            _enemyBase = enemyBase;
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
            
        }

        public override void MoveTo(Vector3 position)
        {
            StateSwitcher.SwitchState<MoveState>();
        }
    }
}