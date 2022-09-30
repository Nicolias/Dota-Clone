using System.Collections;
using UnityEngine;

public class Creep : CharacterBot
{
    public GameObject Viwe => CharacterViwe.gameObject;

    public Creep(SideType side, Stats stats, Base enemyBase, GameObject characterGameObjectOnScene) 
        : base(side, stats, enemyBase, characterGameObjectOnScene)
    {
    }
}