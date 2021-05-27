using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[System.Serializable]
public class HorsePoint
{
    public string name;         //쓰이는 곳이 없음 => 확인용
    public PathType PathType = PathType.Linear;     //직진인지 베지어 곡선인지 확인

    public Transform[] trans;
    public Vector3[] toPoint    //프로퍼티
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

    public HorsePoint(Transform trans)       //연결되지 않은 도로끼리 연결하기 위한 생성자 (구글 검색 : c#생성자)
    {
        this.trans = new Transform[1];
        name = "생성된 직진";
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
        //레이를 쏨
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

        //레이를 쏴서 떨어진 도로끼리 연결함
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
    /// 디버깅용 및 확인용
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
