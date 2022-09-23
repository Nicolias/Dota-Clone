using Characters;
using UnityEngine;
using Servises;
using Assets.Scripts.Character;

public class CharacterBot : Character
{
    private ITarget _enemyBase;

    public CharacterBot(SideType side, Stats stats, Base enemyBase, GameObject characterGameObjectOnScene)
        : base(side, stats, characterGameObjectOnScene)
    {
        _enemyBase = enemyBase;
        CharacterViwe.AgroZone.OnEnemyDetected += Attack;
        //MoveTo(_enemyBase.GameObject.transform.position);
    }
}
