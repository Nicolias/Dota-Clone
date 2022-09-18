using Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
public class AgroZone : MonoBehaviour
{
    public event UnityAction<ITarget> OnEnemyDetected;

    private SideType _ownerSide;

    private void OnTriggerEnter(Collider other)
    {
        var target = other.GetComponentInParent<ITarget>();

        if (target != null & target.Side != _ownerSide)
            OnEnemyDetected?.Invoke(target);
    }
}
