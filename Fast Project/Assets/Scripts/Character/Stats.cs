using System;
using UnityEngine;

[Serializable]
public class Stats
{
    public int Health;
    public int Damage;
    public float AttackDistance;
    public float AttackCoolDown;

    [SerializeField] private float _timePerProcentBeforeAttack;

    public float MoveSpeed;
    public float AttackSpeed;

    public AnimationClip AttackAnimation, DeathAnimation;
    [HideInInspector] public float TimeBeforeAttack => AttackAnimation.length / AttackSpeed / 100f * _timePerProcentBeforeAttack;
}