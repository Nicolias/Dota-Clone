using Zenject;
using UnityEngine;
using Characters;
using Servises;
using System.Collections.Generic;
using System.Collections;
using Assets.Scripts.Character;

public class LocationInstaller : MonoInstaller
{
    [SerializeField] private List<Transform>_lightneesSpawnPoints, _darkneesSpawnPoints;
    [SerializeField] private Transform _playerStartPoint;

    [SerializeField] private List<GameObject>_characterBots;
    [SerializeField] private CharacterScriptableObject _selectedHero;

    [SerializeField] private LightSideBase _lightSideBase;
    [SerializeField] private DarkSideBase _darkSideBase;

    [SerializeField] private GestureClick _terrain;
    private MouseClickServise _mouseClickServise;

    public override void InstallBindings()
    {
        MousClickServisBind();

        MainTowerBind(_lightSideBase);
        MainTowerBind(_darkSideBase);

        PlayerCharacterBind(_selectedHero, _playerStartPoint, SideType.Darkness);
    }

    private void MousClickServisBind()
    {
        MouseClickServise mouseClick = new(_terrain);

        _mouseClickServise = mouseClick;

        Container
            .Bind<MouseClickServise>()
            .FromInstance(mouseClick)
            .AsSingle();
    }

    private void MainTowerBind<T>(T mainTower) where T : Base
    {
        Container
            .Bind<T>()
            .FromInstance(mainTower)
            .AsSingle();
    }

    private void PlayerCharacterBind(CharacterScriptableObject characterInformation, Transform spawnPoint, SideType side)
    {
        GameObject characterGameObject = Container.InstantiatePrefab(characterInformation.CharacterGameobject, spawnPoint.position, Quaternion.identity, null);

        PlayerCharacter playerCharacter = new(side, _mouseClickServise, characterInformation, characterGameObject);

        Container
            .Bind<PlayerCharacter>()
            .FromInstance(playerCharacter)
            .AsCached();
    }
}
