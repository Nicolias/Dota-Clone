using UnityEngine;

namespace Characters
{
    public class CharacterAttacker 
    {
        private Animator _animator;
        private Character _character;

        private float _attackDistanse;

        public CharacterAttacker(Animator animator, Character character)
        {
            _animator = animator;
            _character = character;
        }

        public bool CanAttack(ITarget target)
        {
            return Vector3.Distance(_character.transform.position, target.GameObject.transform.position) <= _attackDistanse;
        }

        public void Attack(ITarget target)
        {
            StartAttackAnimation();
        }

        private void StartAttackAnimation()
        {
            _animator.SetTrigger("Attack");
        }
    }
}
