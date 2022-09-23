using UnityEngine;

namespace Characters.StateMashine
{
    public class AttackState : BaseState
    {
        private Animator _animator;
        private float _nextAttackTime;
        private Stats _stats => Character.Stats;

        public AttackState(Animator animator, IStationStateSwitcher stateSwitcher) : base(stateSwitcher)
        {
            _animator = animator;
        }

        public override void Attack(ITarget target)
        {
            if (Time.time < _nextAttackTime) return;

            _animator.SetTrigger("Attack");

            target.TakeDamage(_stats.Damage);

            _nextAttackTime = Time.time + _stats.AttackCoolDown;
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
            Character.MoveTo(position);
        }
    }
}