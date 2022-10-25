using System;
using UnityEngine;

public abstract class Base : MonoBehaviour, ITarget
{
    public event Action<int> OnHealthChanged;
    public event Action OnDead;

    public GameObject GameObject => gameObject;

    public SideType Side { get; protected set; }

    public int Health => 1000;

    public void TakeDamage(int damageValue)
    {
        throw new System.NotImplementedException();
    }
}
