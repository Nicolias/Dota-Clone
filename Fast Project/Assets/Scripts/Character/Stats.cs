﻿using System;
using UnityEngine;

[Serializable]
public class Stats
{
    public int Health;
    public int Damage;
    public int AttackDistance;
    public float AttackCoolDown;
    public AnimationClip AttackAnimation, DeathAnimation;
}