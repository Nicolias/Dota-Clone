using Character;
using UnityEngine;

public class PromoAnimation : MonoBehaviour
{
    [SerializeField] private CharacterMovment _character;

    private void Start()
    {
        _character.MoveTo(new Vector3(0, 0));
    }
}
