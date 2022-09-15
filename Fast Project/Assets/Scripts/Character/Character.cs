using Servises;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Characters
{
    [RequireComponent(typeof(CharacterMovment))]
    public class Character : MonoBehaviour, ITarget
    {
        private CharacterHealth _characterHealth;
        protected CharacterMovment CharacterMovment;
        protected CharacterAttacker CharacterAttacker;

        private int _health = 10;

        [SerializeField] private AnimationClip _deathAnimation;

        public SideType Side { get; set; }

        public GameObject GameObject => gameObject;        

        private void Awake()
        {
            CharacterMovment = GetComponent<CharacterMovment>();
            CharacterAttacker = new(CharacterMovment.Animator, this);
            _characterHealth = new(CharacterMovment.Animator, _health);
        }

        protected virtual void OnEnable()
        {           
            _characterHealth.OnCharacterDead += () => StartCoroutine(DestroyCharacter());
        }        

        public void TakeDamage(int damageValue)
        {
            _characterHealth.ApllyDamage(damageValue);
        }

        protected void Attack(ITarget target)
        {
            if (target.Side == Side) return;

            if (CharacterAttacker.CanAttack(target))
            {
                CharacterMovment.Stop();
                CharacterAttacker.Attack(target);
            }
            else
            {
                CharacterMovment.MoveTo(target.GameObject.transform.position);
            }
        }

        private IEnumerator DestroyCharacter()
        {
            yield return new WaitForSeconds(_deathAnimation.length);
            Destroy(gameObject);
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.K))
                TakeDamage(100);
        }
    }
}
