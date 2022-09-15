using Characters;
using Servises;
using System.Collections;
using UnityEngine;
using Zenject;

public class PlayerCharacter : Character
{
    private MouseClickServise _mouseClickServise;

    [Inject]
    private void Construct(MouseClickServise mouseClickServise)
    {
        _mouseClickServise = mouseClickServise;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _mouseClickServise.OnTerrainClicked += CharacterMovment.MoveTo;
        _mouseClickServise.OnEnemyClicked += Attack;
    }
    private void OnDisable()
    {
        _mouseClickServise.OnTerrainClicked -= CharacterMovment.MoveTo;
        _mouseClickServise.OnEnemyClicked -= Attack;
    }
}
