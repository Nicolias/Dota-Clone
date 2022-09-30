using System;
using UnityEngine;

[Serializable]
public class Stats
{
    public int Health;
    public int Damage;
    public float AttackDistance;
    public float AttackCoolDown;
    public AnimationClip AttackAnimation, DeathAnimation;
}