using Servises;
using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Zenject;

namespace Characters
{
    [RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
    public class CharacterViwe : MonoBehaviour, ITarget, IPointerClickHandler
    {
        public event UnityAction<int> OnDamageTaken;

        [SerializeField] private AnimationClip _dethAnimation;
        [SerializeField] private AgroZone _agroZone;

        public Character Character { get; set; }

        private MouseClickServise _mouseClickServise;
        private NavMeshAgent _agent;
        private Animator _animator;

        private Vector3 _oldPosition, _newPosition;

        public SideType Side => Character.Side;

        public GameObject GameObject => gameObject;
        public AgroZone AgroZone => _agroZone;
        public NavMeshAgent Agent => _agent;
        public Animator Animator => _animator;

        public int Health => Character.Health;

        public float Speed { get; private set; }

        [Inject]
        public void Cunstract(MouseClickServise mouseClickServise)
        {
            _mouseClickServise = mouseClickServise;
        }

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();            
        }

        private void Update()
        {
            _newPosition = transform.position;
            Speed = Vector3.Distance(_oldPosition, _newPosition) / Time.deltaTime;
            _animator.SetFloat("Speed", Speed);
            _oldPosition = transform.position;
        }

        public void TakeDamage(int damageValue)
        {
            OnDamageTaken?.Invoke(damageValue);
        }

        internal void DestroyCharacter(GameObject characterGameobject)
        {
            Destroy(characterGameobject, _dethAnimation.length);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _mouseClickServise.ClickOnCharcter(this);
        }
    }
}