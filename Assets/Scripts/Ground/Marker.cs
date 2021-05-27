using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;


public class Marker : Connection
{
    public List<Marker> adjacentMarkers;

    public Vector3 Position { get => transform.position; }


    public List<Vector3> GetAdjacentPositions()
    {
        return new List<Vector3>(adjacentMarkers.Select(x => x.Position).ToList());
    }


    public override void FindAndConnection()
    {
        base.FindAndConnection();
        //���̸� ���� ������ ���γ��� ������
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, 1f, rayDir, out hit, 1f, 1 << 3))
        {
            Marker tmp = hit.transform.GetComponent<Marker>();
            adjacentMarkers.Add(tmp);
        }
    }
}
