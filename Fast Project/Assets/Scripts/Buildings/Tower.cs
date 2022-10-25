using Servises;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class Tower : MonoBehaviour, ITarget, IPointerClickHandler
{
    public event Action<int> OnHealthChanged;
    public event Action OnDead;

    [SerializeField] private SideType _side;
    [SerializeField] private int _health;

    [SerializeField] private AgroZone _agroZone;

    [SerializeField] private Vector3 _offset;
    [SerializeField] private FireBall _fireBallPrefab;

    [SerializeField] private float _attackDistance;
        
    private GameObject _currentTarget;
    private FireBall _currentFireBall;
    private MouseClickServise _mouseClickServise;

    public GameObject GameObject => gameObject;

    public SideType Side => _side;

    public int Health => _health;

    [Inject]
    public void Cunstract(MouseClickServise mouseClickServise)
    {
        _mouseClickServise = mouseClickServise;
    }

    private void OnEnable()
    {
        _agroZone.OnEnemyDetected += InstantiateFireBall;
    }

    public void TakeDamage(int damageValue)
    {
        _health -= damageValue;
        OnHealthChanged?.Invoke(_health);

        if (_health <= 0)
        {
            Destroy(gameObject);
            OnDead?.Invoke();
        }
    }

    private void InstantiateFireBall(ITarget target)
    {
        if (target.GameObject == null) return;

        if (_currentFireBall != null)
            return;

        if (_currentTarget == null) 
            _currentTarget = target.GameObject;

        var enemyPosition = target.GameObject.transform.position;

        if (Vector3.Distance(new(enemyPosition.x, enemyPosition.z), new(transform.position.x, transform.position.z)) > _attackDistance)
        {
            _currentTarget = null;
        }
        else
        {
            _currentFireBall = Instantiate(_fireBallPrefab);
            _currentFireBall.transform.position = transform.position + _offset;
            _currentFireBall.Attack(_currentTarget);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _mouseClickServise.ClickOnTarget(this);
    }
}