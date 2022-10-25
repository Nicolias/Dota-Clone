using Assets.Scripts.Character;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Factory
{
    public class CharacterSpawnFactory 
    {
        private DiContainer _diContainer;

        public  CharacterSpawnFactory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public void SpawnCharacters(CharacterScriptableObject[] charactersScriptableObject, Transform[] spawnPositions, Base enemyBase, SideType side)
        {
            for (int i = 0; i < charactersScriptableObject.Length; i++)
            {
                GameObject characterGameObject = _diContainer.InstantiatePrefab(charactersScriptableObject[i].CharacterGameobject, spawnPositions[i]);

                CharacterBot character = new(side, charactersScriptableObject[i].BeginStats, enemyBase, characterGameObject);
            }
        }
    }
}