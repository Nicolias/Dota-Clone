using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Characters.StateMashine
{
    public abstract class BaseState
    {
        protected readonly Character Character;
        protected readonly IStationStateSwitcher StateSwitcher;

        public BaseState(IStationStateSwitcher stateSwitcher)
        {
            StateSwitcher = stateSwitcher;
            Character = (Character)StateSwitcher;
        }

        public abstract void EnterState();
        public abstract void ExitState();

        public abstract void Attack(ITarget target);

        public abstract void MoveTo(Vector3 position);
    }   
}