using Characters;
using Characters.StateMashine;
using Servises;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    private List<BaseState> _allStates;
    protected override List<BaseState> AllStates => _allStates;

    public PlayerCharacter(SideType side, MouseClickServise mouseClickServise, Stats stats, GameObject characterGameObjectOnScene) 
        : base(side, stats, characterGameObjectOnScene)
    {        
        mouseClickServise.OnTerrainClicked += MoveTo;
        mouseClickServise.OnTargetClicked += Attack;

        _allStates = new()
        {
            new IdleState(this),
            new MoveState(Viwe, this),
            new AttackState(Animator, this),
            new DeadState(Viwe, this)
        };

        CurrentState = _allStates[0];
    }

    public override void Attack(ITarget target)
    {
        if (target.Side == Side) return;

        base.Attack(target);
    }
}
