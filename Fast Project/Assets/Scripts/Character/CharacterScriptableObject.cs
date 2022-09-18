using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Character
{
    [CreateAssetMenu(fileName = "Character")]
    public class CharacterScriptableObject : ScriptableObject
    {
        [SerializeField] private Stats _beginStats;
        [SerializeField] private GameObject _characterGameobject;

        public Stats BeginStats => _beginStats;
        public GameObject CharacterGameobject => _characterGameobject;
    }
}
