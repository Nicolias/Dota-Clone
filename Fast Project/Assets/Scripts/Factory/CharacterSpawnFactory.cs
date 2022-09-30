using Assets.Scripts.Character;
using Unity.VisualScripting;
using UnityEngine;

namespace Factory
{
    public static class CharacterSpawnFactory 
    {
        public static void SpawnCharacters(CharacterScriptableObject[] charactersScriptableObject, Transform[] spawnPositions, Base enemyBase, SideType side)
        {
            for (int i = 0; i < charactersScriptableObject.Length; i++)
            {
                GameObject characterGameObject = GameObject.Instantiate(charactersScriptableObject[i].CharacterGameobject, spawnPositions[i]);

                CharacterBot character = new(side, charactersScriptableObject[i].BeginStats, enemyBase, characterGameObject);
            }
        }
    }
}