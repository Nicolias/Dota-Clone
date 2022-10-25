using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepSquad 
{
    private List<Creep> _creepsInSquad;
    
    public CreepSquad(List<Creep> creepsInSquad)
    {
        if (creepsInSquad.Count < 1)
            return;

        _creepsInSquad = creepsInSquad;
        var spawnPosition = GetPosition(_creepsInSquad.Count);

        var senter = _creepsInSquad[0].Viwe.transform.position;

        for (int i = 0; i < creepsInSquad.Count; i++)
            _creepsInSquad[i].Viwe.transform.position = spawnPosition[i] + senter;
    }

    private Vector3[] GetPosition(int count)
    {
        float step = (Mathf.Deg2Rad * 360) / count;
        List<Vector3> result = new();

        for (int i = 0; i < count; i++)
        {
            result.Add(new Vector3(Mathf.Sin(i * step), 0, Mathf.Cos(i * step)));
        }

        return result.ToArray();
    }
}