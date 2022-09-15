using UnityEngine;
using System;

namespace Characters
{
    public class CharacterHealth
    {
        public event Action OnCharacterDead;

        private Animator _animator;
        private int _health;

        public CharacterHealth(Animator animator, int health)
        {
            _animator = animator;
            _health = health;
        }

        public void ApllyDamage(int damage)
        {
            if (_health <= 0) throw new InvalidOperationException("Персонаж уже должен быть мертв");
            if (damage < 0) throw new ArgumentOutOfRangeException("Получен отрицательный урон");

            _health -= damage;

            if (_health <= 0)
            {
                OnCharacterDead?.Invoke();
                _animator.SetTrigger("Dead");
            }
        }
    }
}
