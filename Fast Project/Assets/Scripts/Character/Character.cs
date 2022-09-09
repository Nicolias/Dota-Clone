using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(CharacterMovment))]
    public class Character : MonoBehaviour
    {
        [SerializeField] private GestureClick _terrain;

        private CharacterMovment _characterMovment;

        private void Awake()
        {
            _characterMovment = GetComponent<CharacterMovment>();
        }

        private void OnEnable()
        {
            _terrain.OnClick += _characterMovment.MoveTo;
        }

        private void OnDisable()
        {
            _terrain.OnClick -= _characterMovment.MoveTo;
        }
    }
}
