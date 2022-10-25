using Zenject;
using UnityEngine;
using Servises;
using Assets.Scripts.Character;
using Factory;

public class LocationInstaller : MonoInstaller
{
    [SerializeField] private Transform[]_lightneesSpawnPoints, _darkneesSpawnPoints;

    [SerializeField] private CharacterScriptableObject[] _characterLightBots, _characterDarkBots;
    [SerializeField] private CharacterScriptableObject _selectedHero;

    [SerializeField] private LightSideBase _lightSideBase;
    [SerializeField] private DarkSideBase _darkSideBase;

    [SerializeField] private GestureClick _terrain;
    private MouseClickServise _mouseClickServise;

    [Inject] private DiContainer _diContainer;

    public override void InstallBindings()
    {
        MousClickServisBind();

        MainTowerBind(_lightSideBase);
        MainTowerBind(_darkSideBase);

        PlayerCharacterBind(_selectedHero, _lightneesSpawnPoints[_lightneesSpawnPoints.Length - 1], SideType.Lightness);

        //CharactersSpawn();
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

        PlayerCharacter playerCharacter = new(side, _mouseClickServise, characterInformation.BeginStats, characterGameObject);

        Container
            .Bind<PlayerCharacter>()
            .FromInstance(playerCharacter)
            .AsCached();
    }

    private void CharactersSpawn()
    {
        CharacterSpawnFactory characterSpawnFactory = new(_diContainer);

        characterSpawnFactory.SpawnCharacters(_characterLightBots, _lightneesSpawnPoints, _darkSideBase, SideType.Lightness);
        characterSpawnFactory.SpawnCharacters(_characterDarkBots, _darkneesSpawnPoints, _lightSideBase, SideType.Darkness);
    }
}
