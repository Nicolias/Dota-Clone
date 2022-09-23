using Characters.StateMashine;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Characters
{
    public class Character : IStationStateSwitcher
    {
        private readonly CharacterHealth _characterHealth;
        protected readonly CharacterViwe CharacterViwe;      
        private Stats _stats;

        private BaseState _currentState;
        private List<BaseState> _allStates;

        public SideType Side { get; set; }
        public Stats Stats => _stats;
        public Vector3 Position => CharacterViwe.GameObject.transform.position;

        public Character(SideType side, Stats stats, GameObject characterGameObjectOnScene)
        {
            Side = side;
            _stats = stats;

            CharacterViwe = characterGameObjectOnScene.GetComponent<CharacterViwe>();
            _characterHealth = new(CharacterViwe.Animator, _stats.Health);

            CharacterViwe.Side = Side;
            CharacterViwe.GetHealth = () => _characterHealth.Health;
            
            CharacterViwe.OnDamageTaken += TakeDamage;

            _characterHealth.OnCharacterDead += () =>
            {
                CharacterViwe.DestroyCharacter(CharacterViwe.gameObject);
                CharacterViwe.OnDamageTaken -= TakeDamage;
            };

            _allStates = new()
            {
                new IdleState(this),
                new AttackState(CharacterViwe.Animator, this),
                new MoveState(CharacterViwe, this)
            };

            _currentState = _allStates[0];
        }

        public void TakeDamage(int damageValue)
        {
            _characterHealth.ApllyDamage(damageValue);
        }

        public void MoveTo(Vector3 position)
        {
            _currentState.MoveTo(position);
        }

        public void Attack(ITarget target)
        {
            if (target == (ITarget)CharacterViwe) return;           

            if (target.Side == Side) throw new InvalidOperationException("Нельзя бить союзника");

            if (Vector3.Distance(target.GameObject.transform.position, Position) > _stats.AttackDistance)
                MoveTo(target.GameObject.transform.position);
            else
                _currentState.Attack(target);
        }

        public void SwitchState<T>() where T : BaseState
        {
            _currentState.ExitState();
            var state = _allStates.FirstOrDefault(s => s is T);
            state.EnterState();
            _currentState = state;
        }
    }
}
