using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
public class AgroZone : MonoBehaviour
{
    public event UnityAction<ITarget> OnEnemyDetected;

    private SideType _ownerSide;

    private SphereCollider _sphereCollider;
    public SphereCollider SphereCollider => _sphereCollider;

    private void Awake()
    {
        _sphereCollider = GetComponent<SphereCollider>();
        _sphereCollider.enabled = false;
    }

    private void Start()
    {
        _ownerSide = GetComponentInParent<ITarget>().Side;

        _sphereCollider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        var target = other.GetComponentInParent<ITarget>();

        if (target == null) return;

        if (target.Side != _ownerSide)
            OnEnemyDetected?.Invoke(target);
    }

    private void OnTriggerStay(Collider other)
    {
        OnTriggerEnter(other);
    }
}
