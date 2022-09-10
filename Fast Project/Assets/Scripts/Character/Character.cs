using Servises;
using UnityEngine;
using Zenject;

namespace Characters
{
    public class Character : MonoBehaviour
    {
        private MouseClickServise _mouseClickServise;
        private CharacterMovment _characterMovment;
        private CharacterAttacker _characterAttacker;

        [Inject]
        private void Construct(MouseClickServise mouseClickServise)
        {
            _mouseClickServise = mouseClickServise;
        }

        private void Awake()
        {
            _characterMovment = GetComponent<CharacterMovment>();
            _characterAttacker = new(_characterMovment.Animator, this);
        }

        private void OnEnable()
        {
            _mouseClickServise.OnTerrainClicked += _characterMovment.MoveTo;
            _mouseClickServise.OnEnemyClicked += Attack;
        }

        private void OnDisable()
        {
            _mouseClickServise.OnTerrainClicked -= _characterMovment.MoveTo;
            _mouseClickServise.OnEnemyClicked -= Attack;
        }

        private void Attack(Enemy enemy)
        {
            if (_characterAttacker.CanAttackEnemy(enemy))
            {
                _characterMovment.Stop();
                _characterAttacker.Attack(enemy);
            }
            else
            {
                _characterMovment.MoveTo(enemy.transform.position);
            }
        }
    }
}
