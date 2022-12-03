using UnityEngine;
using System.Collections;
using System;

namespace Characters.StateMashine
{
    public class AttackState : BaseState
    {
        private Animator _animator;
        private float _nextAttackTime;

        private ITarget _currentTarget;

        private Stats _stats => Character.Stats;

        public AttackState(Animator animator, IStationStateSwitcher stateSwitcher) : base(stateSwitcher)
        {
            _animator = animator;
        }

        public override void Attack(ITarget target)
        {
            if(_currentTarget != null)
                _currentTarget.OnDead -= SwithcToMoveState;

            _currentTarget = target;
            _currentTarget.OnDead += SwithcToMoveState;

            if (target == (ITarget)Character.Viwe) return;

            if (target.Side == Character.Side) throw new InvalidOperationException("Нельзя бить союзника");

            var enemyPosition = target.GameObject.transform.position;

            if (Vector3.Distance(new(enemyPosition.x, enemyPosition.z), new(Character.Position.x, Character.Position.z)) >= _stats.AttackDistance)
                Character.Viwe.TargetForFolow = target.GameObject;
            else
                AttackTarget(target);           
        }

        public override void EnterState()
        {
            
        }

        public override void ExitState()
        {
            Character.Viwe.Agent.ResetPath();

            _currentTarget.OnDead -= SwithcToMoveState;     
        }

        public override void MoveTo(Vector3 position)
        {
            if(Character.Viwe.Agent.destination != position)
                Character.Viwe.Agent.SetDestination(position);
        }

        private void AttackTarget(ITarget target)
        {
            Character.Viwe.TargetForFolow = null;
            Character.Viwe.Agent.ResetPath();

            Character.Viwe.transform.rotation = Quaternion.Slerp(Character.Viwe.transform.rotation, Quaternion.LookRotation(target.GameObject.transform.position - Character.Viwe.transform.position), 1);

            if (Time.time < _nextAttackTime) return;

            _animator.SetTrigger("Attack");

            Character.Viwe.StartCoroutine(AttackAfterSeconds(_stats.TimeBeforeAttack, target));

            _nextAttackTime = Time.time + _stats.AttackAnimation.length / _stats.AttackSpeed + _stats.AttackCoolDown;
        }

        private IEnumerator AttackAfterSeconds(float timeBeforeAttack, ITarget target)
        {
            yield return new WaitForSeconds(timeBeforeAttack);

            if (target.Health > 0)            
                target.TakeDamage(_stats.Damage);

            if (target.Health <= 0)
                SwithcToMoveState();
        }

        private void SwithcToMoveState()
        {
            Character.SwitchState<MoveState>();
        }
    }
}