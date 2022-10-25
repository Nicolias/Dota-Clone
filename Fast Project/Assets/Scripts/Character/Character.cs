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
        public readonly CharacterViwe Viwe;
        protected readonly Animator Animator;

        private Stats _stats;

        protected abstract List<BaseState> AllStates { get; }
        protected BaseState CurrentState;

        public SideType Side { get; set; }
        public Stats Stats => _stats;
        public Vector3 Position => Viwe.GameObject.transform.position;
        public int Health => _characterHealth.Health;

        public Character(SideType side, Stats stats, GameObject characterGameObjectOnScene)
        {
            Side = side;
            _stats = stats;

            Viwe = characterGameObjectOnScene.GetComponent<CharacterViwe>();
            Viwe.Character = this;
            Viwe.OnDamageTaken += TakeDamage;
            Animator = Viwe.Animator;

            _characterHealth = new(Animator, _stats.Health);
            _characterHealth.OnCharacterDead += () =>
            {
                SwitchState<DeadState>();
                Viwe.DestroyCharacter();
                Viwe.OnDamageTaken -= TakeDamage;
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

        public virtual void Attack(ITarget target)
        {
            if(CurrentState is not AttackState)
                SwitchState<AttackState>();

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
