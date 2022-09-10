using Zenject;
using UnityEngine;
using Characters;
using Servises;

public class LocationInstaller : MonoInstaller
{
    [SerializeField] private Transform _startPoint;
    [SerializeField] private GameObject _heroPrefab;
    [SerializeField] private GestureClick _terrain;

    public override void InstallBindings()
    {
        BindTMousClickServis();

        CharacterBind();
    }

    private void BindTMousClickServis()
    {
        MouseClickServise mouseClick = new(_terrain);

        Container
            .Bind<MouseClickServise>()
            .FromInstance(mouseClick)
            .AsSingle();
    }

    private void CharacterBind()
    {
        Character character = Container.InstantiatePrefabForComponent<Character>(_heroPrefab, _startPoint.position, Quaternion.identity, null);

        Container
            .Bind<Character>()
            .FromInstance(character)
            .AsSingle();
    }
}
