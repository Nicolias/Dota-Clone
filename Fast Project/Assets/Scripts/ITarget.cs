using System;
using UnityEngine;

public interface ITarget
{
    public event Action<int> OnHealthChanged;
    public event Action OnDead;

    public GameObject GameObject { get; }
    public SideType Side { get; }
    public int Health { get; }

    public void TakeDamage(int damageValue);
}