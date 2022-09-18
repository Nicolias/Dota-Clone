using Assets.Scripts.Character;
using Servises;
using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Characters
{
    [RequireComponent(typeof(CharacterMovment))]
    public class Character : ITarget, IPointerClickHandler
    {
        protected readonly MouseClickServise MouseClickServise;
        private readonly CharacterScriptableObject _characterInformation;
        private readonly GameObject _gameObject;

        private Stats _stats;
        private CharacterHealth _characterHealth;
        protected CharacterMovment CharacterMovment;
        protected CharacterAttacker CharacterAttacker;

        public SideType Side { get; set; }
        public GameObject GameObject => _gameObject;       

        public Character(SideType side, MouseClickServise mouseClickServise, CharacterScriptableObject characterScriptableObject, GameObject characterGameObjectOnScene)
        {
            Side = side;
            _characterInformation = characterScriptableObject;
            MouseClickServise = mouseClickServise;
            _gameObject = characterGameObjectOnScene;
            _stats = _characterInformation.BeginStats;

            CharacterMovment = characterGameObjectOnScene.GetComponent<CharacterMovment>();
            CharacterAttacker = new(this, _stats.AttackDistance, _stats.Damage);
            _characterHealth = new(CharacterMovment.Animator, _stats.Health);
            
            _characterHealth.OnCharacterDead += () => CharacterMovment.DestroyCharacter(GameObject);
        }

        public void TakeDamage(int damageValue)
        {
            _characterHealth.ApllyDamage(damageValue);
        }

        protected void Attack(ITarget target)
        {
            if (target.Side == Side) throw new InvalidOperationException("Нельзя бить союзника");

            if (CharacterAttacker.CanAttack(target) == false)
            {
                CharacterMovment.Animator.SetTrigger("Attack");
                CharacterMovment.Stop();
                CharacterAttacker.Attack(target);
            }

            CharacterMovment.MoveTo(target.GameObject.transform.position);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            MouseClickServise.ClickOnCharcter(this);
        }
    }
}
