using Assets.Scripts.Character;
using Characters;
using Servises;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class PlayerCharacter : Character, IPointerClickHandler
{
    public PlayerCharacter(SideType side, MouseClickServise mouseClickServise, CharacterScriptableObject characterScriptableObject, GameObject characterGameObjectOnScene) 
        : base(side, mouseClickServise, characterScriptableObject, characterGameObjectOnScene)
    {        
        MouseClickServise.OnTerrainClicked += CharacterMovment.MoveTo;
        MouseClickServise.OnCharacterClicked += Attack;
    }
}
