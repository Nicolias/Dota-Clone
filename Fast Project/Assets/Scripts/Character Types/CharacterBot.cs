using Characters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Zenject;

public class CharacterBot : Character
{
    private ITarget _mainEnemyTower;

    private List<Tower> _mainTowers;

    [Inject]
    private void Construct(List<Tower> mainTowers)
    {
        _mainTowers = mainTowers;
    }

    private void Start()
    {
        foreach (var mainTower in _mainTowers)
        {
            if (mainTower.Side != Side)
                _mainEnemyTower = mainTower;
        }   
        
        CharacterMovment.MoveTo(_mainEnemyTower.GameObject.transform.position);
    }
}
