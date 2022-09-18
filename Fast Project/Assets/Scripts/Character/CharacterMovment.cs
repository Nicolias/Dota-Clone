using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Characters
{
    [RequireComponent(typeof(NavMeshAgent), typeof(Animator))]
    public class CharacterMovment : MonoBehaviour
    {
        [SerializeField] private AnimationClip _dethAnimation;

        private NavMeshAgent _agent;
        private Animator _animator;

        private Vector3 _oldPosition, _newPosition;

        public Animator Animator => _animator;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _animator.enabled = true;
        }

        private void OnDisable()
        {
            _animator.enabled = false;
        }

        internal void DestroyCharacter(GameObject characterGameobject)
        {
            A(characterGameobject);

            IEnumerator A(GameObject gameObject)
            {
                yield return new WaitForSeconds(_dethAnimation.length);
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            _newPosition = transform.position;
            _animator.SetFloat("Speed", Vector3.Distance(_newPosition, _oldPosition) / Time.deltaTime);
            _oldPosition = _newPosition;
        }

        public void MoveTo(Vector3 targetPoint)
        {
            _agent.SetDestination(targetPoint);
        }

        public void Stop()
        {
            _agent.ResetPath();
        }
    }
}
