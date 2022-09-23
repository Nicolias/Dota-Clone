using UnityEngine;

public abstract class Base : MonoBehaviour, ITarget
{
    public GameObject GameObject => gameObject;

    public SideType Side { get; protected set; }

    public int Health => 1000;

    public void TakeDamage(int damageValue)
    {
        throw new System.NotImplementedException();
    }
}
