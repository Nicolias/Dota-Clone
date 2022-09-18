using System;
using UnityEngine;

namespace Characters
{
    public class CharacterAttacker 
    {
        private Character _character;

        private float _attackDistance;
        private int _damage;

        public CharacterAttacker(Character character, float attackDistance, int damageValue)
        {
            _character = character;
            _attackDistance = attackDistance;
            _damage = damageValue;
        }

        public bool CanAttack(ITarget target)
        {
            return Vector3.Distance(_character.GameObject.transform.position, target.GameObject.transform.position) <= _attackDistance;
        }

        public void Attack(ITarget target)
        {
            if (CanAttack(target) == false) throw new InvalidOperationException("До цели слишком далеко");

            target.TakeDamage(_damage);
        }
    }
}
