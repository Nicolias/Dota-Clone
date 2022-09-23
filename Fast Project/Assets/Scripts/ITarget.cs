using Characters;
using UnityEngine;

public interface ITarget
{
    public GameObject GameObject { get; }
    public SideType Side { get; }
    public int Health { get; }

    public void TakeDamage(int damageValue);
}