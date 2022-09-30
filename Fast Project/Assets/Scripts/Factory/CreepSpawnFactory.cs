using Assets.Scripts.Character;
using System.Collections;
using UnityEngine;

namespace Factory
{
    public class CreepSpawnFactory : MonoBehaviour
    {
        [SerializeField] private float _spawnDelay;
        [SerializeField] private CreepScriptableObject _creep;
        [SerializeField] private Base _enemyBase;
        [SerializeField] private SideType _creepSide;

        private void Start()
        {
            StartCoroutine(SpawnCreeps());
        }

        private IEnumerator SpawnCreeps()
        {
            while (true)
            {
                yield return new WaitForSeconds(_spawnDelay);

                new Creep(_creepSide, _creep.BeginStats, _enemyBase, Instantiate(_creep.CreepGameobject, transform));
            }
        }
    }
}