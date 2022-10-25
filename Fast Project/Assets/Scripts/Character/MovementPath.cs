using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class MovementPath
{
    [SerializeField] private Transform[] _pathPoints;
    private int _nexPathPoint;

    public Transform[] PathPoints => _pathPoints;

    public MovementPath(Transform[] pathPoints)
    {
        _pathPoints = pathPoints;
    }

    public IEnumerator<Transform> GetNextPathPoint()
    {
        if (_pathPoints == null || _pathPoints.Length < 1)
            yield break;

        while (_pathPoints.Length > _nexPathPoint)
        {
            yield return _pathPoints[_nexPathPoint];
            _nexPathPoint++;
        }
    }
}