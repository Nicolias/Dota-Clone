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
        public event Action<int> OnHealthChanged;
        public event Action<int> OnDamageTaken;
        public event Action OnPositionReached;
        public event Action OnDead;

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

        [SerializeField] private float _distanceAfteWhichChangeDestination;
        [HideInInspector] public Transform DestinationPoint { get; set; }

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

        private void Start()
        {
            _agent.speed = Character.Stats.MoveSpeed;            
        }

        private void Update()
        {
            CalculateSpeed();

            if (DestinationPoint != null)
                MoveToPoint(DestinationPoint.position);
        }

        public void TakeDamage(int damageValue)
        {
            OnDamageTaken?.Invoke(damageValue);
            OnHealthChanged?.Invoke(Health);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _mouseClickServise.ClickOnTarget(this);
        }

        private void CalculateSpeed()
        {
            _newPosition = transform.position;
            Speed = Vector3.Distance(_oldPosition, _newPosition) / Time.deltaTime;
            _animator.SetFloat("Speed", Speed);
            _oldPosition = transform.position;
        }
        
        private void MoveToPoint(Vector3 destinationPoint)
        {            
            transform.SetPositionAndRotation(
                Vector3.MoveTowards(transform.position, destinationPoint, Time.deltaTime * Character.Stats.MoveSpeed), 
                Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(destinationPoint - transform.position), 1));

            if ((transform.position - DestinationPoint.position).sqrMagnitude < _distanceAfteWhichChangeDestination * _distanceAfteWhichChangeDestination)
                OnPositionReached?.Invoke();
        }

        internal void DestroyCharacter()
        {
            Destroy(gameObject, Character.Stats.DeathAnimation.length);
            enabled = false;
        }
    }
}