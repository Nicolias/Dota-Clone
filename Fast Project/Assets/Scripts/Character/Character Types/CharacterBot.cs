using Characters;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using Servises;
using Assets.Scripts.Character;

public class CharacterBot : Character, IPointerClickHandler
{
    private ITarget _enemyBase;

    public CharacterBot(SideType side, MouseClickServise mouseClickServise, CharacterScriptableObject characterScriptableObject, Base enemyBase, GameObject characterGameObjectOnScene)
        : base(side, mouseClickServise, characterScriptableObject, characterGameObjectOnScene)
    {
        _enemyBase = enemyBase;

        CharacterMovment.GetComponentInChildren<AgroZone>().OnEnemyDetected += Attack;
    }

    private void Start()
    {        
        CharacterMovment.MoveTo(_enemyBase.GameObject.transform.position);
    }
}
