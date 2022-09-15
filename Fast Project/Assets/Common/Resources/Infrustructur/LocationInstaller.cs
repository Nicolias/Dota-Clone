using Zenject;
using UnityEngine;
using Characters;
using Servises;
using System.Collections.Generic;
using System.Collections;

public class LocationInstaller : MonoInstaller
{
    [SerializeField] private List<Transform>_lightneesSpawnPoints, _darkneesSpawnPoints;
    [SerializeField] private Transform _playerStartPoint;

    [SerializeField] private List<Character>_characterBots;
    [SerializeField] private Character _heroPrefab;

    [SerializeField] private List<Tower> _mainTowers;

    [SerializeField] private GestureClick _terrain;

    public override void InstallBindings()
    {
        MousClickServisBind();

        MainTowerBind();

        CharacterBind<PlayerCharacter>(_heroPrefab, _playerStartPoint, SideType.Lightness);

        for (int i = 0; i < _characterBots.Count; i++)
        {
            CharacterBind<CharacterBot>(_characterBots[i], _lightneesSpawnPoints[i], SideType.Lightness);
        }
    }

    private void MousClickServisBind()
    {
        MouseClickServise mouseClick = new(_terrain);

        Container
            .Bind<MouseClickServise>()
            .FromInstance(mouseClick)
            .AsSingle();
    }

    private void MainTowerBind()
    {
        Container
            .Bind<List<Tower>>()
            .FromInstance(_mainTowers)
            .AsSingle();
    }

    private void CharacterBind<T>(Character characterPrefab, Transform spawnPoint, SideType side) where T : Character
    {
        if (characterPrefab.GetComponent<T>() == null)
            characterPrefab.gameObject.AddComponent<T>();

        T character = Container.InstantiatePrefabForComponent<T>(characterPrefab.gameObject, spawnPoint.position, Quaternion.identity, null);
        character.Side = side;

        Container
            .Bind<T>()
            .FromInstance(character)
            .AsCached();

    }
}
