using System.Collections;
using UnityEngine;

public class DistanceComperer : IComparer
{
    private Transform _compareTransform;

    public DistanceComperer(Transform comparerTransform)
    {
        _compareTransform = comparerTransform;
    }

    public int Compare(object x, object y)
    {
        Collider xCollider = x as Collider;
        Collider yCollider = y as Collider;

        Vector3 offset = xCollider.transform.position - _compareTransform.position;
        float xDistance = offset.sqrMagnitude;

        offset = yCollider.transform.position - _compareTransform.position;
        float yDistance = offset.sqrMagnitude;

        return xDistance.CompareTo(yDistance);
    }
}