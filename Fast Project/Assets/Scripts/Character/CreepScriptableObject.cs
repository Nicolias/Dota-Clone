using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Character
{
    [CreateAssetMenu(fileName = "Creep")]
    public class CreepScriptableObject : ScriptableObject
    {
        [SerializeField] private Stats _beginStats;
        [SerializeField] private GameObject _creepGameobject;

        public Stats BeginStats => _beginStats;
        public GameObject CreepGameobject => _creepGameobject;
    }
}