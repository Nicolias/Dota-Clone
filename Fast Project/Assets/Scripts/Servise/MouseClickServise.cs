using Characters;
using System;
using UnityEngine;

namespace Servises
{
    public class MouseClickServise
    {
        public event Action<Vector3> OnTerrainClicked;
        public event Action<ITarget> OnCharacterClicked;

        private GestureClick _terrain;

        public MouseClickServise(GestureClick terrain)
        {
            _terrain = terrain;
            _terrain.OnClick += (position) => OnTerrainClicked?.Invoke(position);
        }

        public void ClickOnCharcter(ITarget character)
        {
            OnCharacterClicked?.Invoke(character);
        }
    }
}
