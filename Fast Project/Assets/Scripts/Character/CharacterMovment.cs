using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMovment : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Animator _animator;

    private Vector3 _oldPosition, _newPosition;

    private void OnEnable()
    {
        _animator.enabled = true; 
    }

    private void OnDisable()
    {
        _animator.enabled = false;
    }

    public void MoveTo(Vector3 targetPoint)
    {
        _agent.SetDestination(targetPoint);        
    }

    private void Update()
    {
        _newPosition = transform.position;
        _animator.SetFloat("Speed", Vector3.Distance(_newPosition, _oldPosition) / Time.deltaTime);
        _oldPosition = transform.position;
    }
}
