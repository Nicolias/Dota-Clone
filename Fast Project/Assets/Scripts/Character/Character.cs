using Characters.StateMashine;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Characters
{
    public abstract class Character : IStationStateSwitcher
    {
        private readonly CharacterHealth _characterHealth;
        protected readonly CharacterViwe CharacterViwe;
        protected readonly Animator Animator;

        private Stats _stats;

        protected abstract List<BaseState> AllStates { get; }
        protected BaseState CurrentState;

        public SideType Side { get; set; }
        public Stats Stats => _stats;
        public Vector3 Position => CharacterViwe.GameObject.transform.position;
        public int Health => _characterHealth.Health;

        public Character(SideType side, Stats stats, GameObject characterGameObjectOnScene)
        {
            Side = side;
            _stats = stats;

            CharacterViwe = characterGameObjectOnScene.GetComponent<CharacterViwe>();
            CharacterViwe.Character = this;
            CharacterViwe.OnDamageTaken += TakeDamage;
            Animator = CharacterViwe.Animator;

            _characterHealth = new(Animator, _stats.Health);
            _characterHealth.OnCharacterDead += () =>
            {
                CharacterViwe.DestroyCharacter(CharacterViwe.gameObject);
                CharacterViwe.OnDamageTaken -= TakeDamage;
            };
        }

        public void TakeDamage(int damageValue)
        {
            _characterHealth.ApllyDamage(damageValue);
        }

        public void MoveTo(Vector3 position)
        {
            CurrentState.MoveTo(position);
        }

        public void Attack(ITarget target)
        {
            if (target == (ITarget)CharacterViwe) return;

            if (target.Side == Side) throw new InvalidOperationException("Нельзя бить союзника");

            if (target.Health <= 0)
            {
                SwitchState<IdleState>();
                return;
            }

            var enemyPosition = target.GameObject.transform.position;

            Debug.Log(Position);
            

            if (Vector3.Distance(new(enemyPosition.x, enemyPosition.z), new(Position.x, Position.z)) > _stats.AttackDistance)
                MoveTo(target.GameObject.transform.position);
            else
                CurrentState.Attack(target);
        }

        public void SwitchState<T>() where T : BaseState
        {
            CurrentState.ExitState();
            var state = AllStates.FirstOrDefault(s => s is T);
            state.EnterState();
            CurrentState = state;
        }
    }
}
