using System.Collections.Generic;
using UnityEngine;

namespace Characters.StateMashine
{
    public class CreepMoveState : MoveState
    {
        private readonly CharacterViwe _characterViwe;

        private MovementPath _path;
        private IEnumerator<Transform> _pointInPath;

        public CreepMoveState(IStationStateSwitcher stateSwitcher, CharacterViwe characterViwe, MovementPath path) : base(characterViwe, stateSwitcher)
        {
            _path = path;

            _pointInPath = _path.GetNextPathPoint();
            _pointInPath.MoveNext();

            _characterViwe = characterViwe;
            _characterViwe.transform.position = _pointInPath.Current.position;
        }

        public override void Attack(ITarget target)
        {
            Character.SwitchState<AttackState>();
            Character.Attack(target);
        }

        public override void EnterState()
        {
            _characterViwe.DestinationPoint = _pointInPath.Current;
            _characterViwe.OnPositionReached += ChangePointInPath;
            _characterViwe.Agent.ResetPath();
        }

        public override void ExitState()
        {
            _characterViwe.DestinationPoint = null;
            _characterViwe.OnPositionReached -= ChangePointInPath;
        }

        public override void MoveTo(Vector3 position)
        {
            _characterViwe.DestinationPoint = null;
            _characterViwe.Agent.SetDestination(position);
        }

        private void ChangePointInPath()
        {
            _pointInPath.MoveNext();
            _characterViwe.DestinationPoint = _pointInPath.Current;            
        }
    }
}