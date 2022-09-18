﻿using Characters;
using UnityEditor;
using UnityEngine;
public class Tower : MonoBehaviour, ITarget
{
    [SerializeField] private SideType _side;

    public GameObject GameObject => gameObject;

    public SideType Side => _side;

    public void TakeDamage(int damageValue)
    {
        throw new System.NotImplementedException();
    }
}