using Assets.Scripts.Character;
using Servises;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Factory
{
    public class CreepSpawnFactory : MonoBehaviour
    {
        [SerializeField] private float _spawnDelay;
        [SerializeField] private CreepScriptableObject _creep;
        [SerializeField] private Base _enemyBase;
        [SerializeField] private SideType _creepSide;

        [SerializeField] private int _squadCount;

        [SerializeField] private MovementPath _creepPath;

        private CreepSquad _currentSquad;

        private DiContainer _diContainer;

        [Inject]
        private void Construct(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        private void Start()
        {
            StartCoroutine(SpawnCreeps());
        }

        private IEnumerator SpawnCreeps()
        {
            while (true)
            {
                List<Creep> creepsInSquad = new();

                for (int i = 0; i < _squadCount; i++)
                {
                    var creepGameObjectOnScen = _diContainer.InstantiatePrefab(_creep.CreepGameobject, transform);
                    creepsInSquad.Add(new Creep(_creepSide, _creep.BeginStats, _enemyBase, creepGameObjectOnScen, new(_creepPath.PathPoints)));
                }

                _currentSquad = new(creepsInSquad);

                yield return new WaitForSeconds(_spawnDelay);
            }
        }       

        public void OnDrawGizmos()
        {
            if (_creepPath.PathPoints.Length <= 2 || _creepPath.PathPoints == null)
                return;

            for (int i = 1; i < _creepPath.PathPoints.Length; i++)
            {
                Gizmos.DrawLine(_creepPath.PathPoints[i - 1].position, _creepPath.PathPoints[i].position);
            }
        }
    }
}