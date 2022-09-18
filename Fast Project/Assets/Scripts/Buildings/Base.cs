using UnityEngine;

public abstract class Base : MonoBehaviour, ITarget
{
    [SerializeField] private SideType _side;

    public GameObject GameObject => gameObject;

    public SideType Side => _side;

    public void TakeDamage(int damageValue)
    {
        throw new System.NotImplementedException();
    }
}
