using UnityEngine;
public class Tower : MonoBehaviour, ITarget
{
    [SerializeField] private SideType _side;
    [SerializeField] private int _health;

    public GameObject GameObject => gameObject;

    public SideType Side => _side;

    public int Health => _health;

    public void TakeDamage(int damageValue)
    {
        _health -= damageValue;
    }
}
