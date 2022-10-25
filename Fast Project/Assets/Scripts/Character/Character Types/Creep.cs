using Characters;
using Characters.StateMashine;
using System.Collections.Generic;
using UnityEngine;

public class Creep : Character
{
    protected override List<BaseState> AllStates => _allStats;
    private List<BaseState> _allStats;

    public Creep(SideType side, Stats stats, Base enemyBase, GameObject characterGameObjectOnScene, MovementPath creepPath)
         : base(side, stats, characterGameObjectOnScene)
    {
        Viwe.AgroZone.OnEnemyDetected += base.Attack;

        _allStats = new()
        {
            new IdleState(this, enemyBase),
            new CreepMoveState(this, Viwe, creepPath),
            new AttackState(Animator, this),
            new DeadState(Viwe, this)
        };

        CurrentState = AllStates[0];

        SwitchState<CreepMoveState>();
    }
}