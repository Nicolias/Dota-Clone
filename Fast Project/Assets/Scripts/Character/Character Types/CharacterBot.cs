using Characters;
using Characters.StateMashine;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBot : Character
{
    private List<BaseState> _allStats;
    protected override List<BaseState> AllStates => _allStats;

    public CharacterBot(SideType side, Stats stats, Base enemyBase, GameObject characterGameObjectOnScene)
        : base(side, stats, characterGameObjectOnScene)
    {
        Viwe.AgroZone.OnEnemyDetected += Attack;

        _allStats = new()
        {
            new IdleState(this, enemyBase),
            new MoveState(Viwe, this),
            new AttackState(Animator, this),
            new DeadState(Viwe, this)
        };

        CurrentState = AllStates[0];

        SwitchState<IdleState>();
    }

}
