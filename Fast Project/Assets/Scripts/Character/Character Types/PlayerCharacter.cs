using Characters;
using Servises;
using UnityEngine;

public class PlayerCharacter : Character
{
    public PlayerCharacter(SideType side, MouseClickServise mouseClickServise, Stats stats, GameObject characterGameObjectOnScene) 
        : base(side, stats, characterGameObjectOnScene)
    {        
        mouseClickServise.OnTerrainClicked += MoveTo;
        mouseClickServise.OnCharacterClicked += Attack;
    }
}
