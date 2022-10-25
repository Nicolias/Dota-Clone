using Characters;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class FireBall : MonoBehaviour
{
    [SerializeField] private int _damageValue;
    [SerializeField] private float _speed;

    private GameObject _target;

    private Vector3 _direction;

    public void Attack(GameObject target)
    {
        _target = target;
    }

    private void LateUpdate()
    {
        if (_target != null) 
            _direction = _target.transform.position - transform.position;

        transform.position += _direction.normalized * Time.deltaTime * _speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out CharacterViwe enemy))
        {
            enemy.TakeDamage(_damageValue);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}