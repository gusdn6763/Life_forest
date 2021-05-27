using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[System.Serializable]
public class HorsePoint
{
    public string name;         //���̴� ���� ���� => Ȯ�ο�
    public PathType PathType = PathType.Linear;     //�������� ������ ����� Ȯ��

    public Transform[] trans;
    public Vector3[] toPoint    //������Ƽ
    {
        get
        {
            Vector3[] waypoints = new Vector3[trans.Length];
            for (int i = 0; i < trans.Length; i++)
            {
                waypoints[i] = trans[i].position;
            }
            return waypoints;
        }
    }

    public HorsePoint(Transform trans)       //������� ���� ���γ��� �����ϱ� ���� ������ (���� �˻� : c#������)
    {
        this.trans = new Transform[1];
        name = "������ ����";
        this.trans[0] = trans;
        PathType = PathType.Linear;
    }

}

public class HorseMove : MonoBehaviour
{
    [SerializeField] private RayPoint rayPoint = RayPoint.NONE;
    private Vector3 rayDir;

    public List<HorsePoint> wayPoints;

    public void FindAndConnection()
    {
        //���̸� ��
        switch (rayPoint)
        {
            case RayPoint.LEFT:
                rayDir = -transform.forward;
                break;
            case RayPoint.RIGHT:
                rayDir = transform.forward;
                break;
            case RayPoint.TOP:
                rayDir = -transform.right;
                break;
            case RayPoint.BOTTOM:
                rayDir = transform.right;
                break;
        }

        //���̸� ���� ������ ���γ��� ������
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, 1f, rayDir, out hit, 12f, 1 << 3))
        {
            HorseMove tmp = hit.transform.GetComponent<HorseMove>();
            if (wayPoints.Count == 0 || tmp.wayPoints.Count == 3)
            {
                wayPoints.Add(new HorsePoint(tmp.transform));
            }
            else if (tmp.wayPoints.Count == 0)
            {
                tmp.wayPoints.Add(wayPoints[0]);
            }
        }
    }

    /// <summary>
    /// ������ �� Ȯ�ο�
    /// </summary>
    private void OnDrawGizmos()
    {
        switch (rayPoint)
        {
            case RayPoint.LEFT:
                rayDir = -transform.forward;
                break;
            case RayPoint.RIGHT:
                rayDir = transform.forward;
                break;
            case RayPoint.TOP:
                rayDir = -transform.right;
                break;
            case RayPoint.BOTTOM:
                rayDir = transform.right;
                break;
        }

        RaycastHit hit;
        if (Physics.SphereCast(transform.position, 1f, rayDir, out hit, 12, 1 << 3))
        {
        }
        else
        {
            Gizmos.DrawRay(transform.position, rayDir * 12f);
        }
    }
}
