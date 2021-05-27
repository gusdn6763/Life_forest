using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Horse : MonoBehaviour
{
    [SerializeField] private float collisionRaycastLength;
    [SerializeField] private float speed = 8f;                //속도

    private HorseMove currentPoint;           //현재 지점 -> 목표 지점
    private HorseMove nextPoint;              //목표 지점
    private bool stop;                      //말들의 충돌, 멈추기위한 stop

    public bool Stop
    {
        get { return stop; }
        set 
        {
            stop = value; 
            if (stop) transform.DOPause(); 
            else  transform.DOPlay(); 
        }
    }

    private void Start()
    {
        MoveHorse();
    }


    /// <summary>
    /// 차이동
    /// </summary>
    public void MoveHorse()
    {
        int currentCount = Random.Range(0, currentPoint.wayPoints.Count);
        HorsePoint nextWayPoint = currentPoint.wayPoints[currentCount];

        transform.DOPath(nextWayPoint.toPoint, speed, nextWayPoint.PathType).
        SetLookAt(0.1f).SetEase(Ease.Linear).SetSpeedBased(true).OnComplete(() =>
        {
            nextPoint = nextWayPoint.trans[0].GetComponent<HorseMove>();     //다음 위치값을 받음

            currentPoint = nextPoint;       //현재 지점 = 목표지점
            if (currentPoint.wayPoints.Count == 0)      //인스펙터에서 값을 안 넣어줬거나 도로 끝으로 이동할경우
            {
                print(currentPoint.transform.gameObject);
                print("길이 막힘");
                Destroy(this.gameObject, 1f);
            }
            else
            {
                currentCount = Random.Range(0, currentPoint.wayPoints.Count);
                nextWayPoint = currentPoint.wayPoints[currentCount];
                MoveHorse();
                nextPoint = nextWayPoint.trans[0].GetComponent<HorseMove>();
            }
        });
    }

    public void SetStartPosition(HorseMove roadMove)
    {
        currentPoint = roadMove;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.horse))
        {
            Stop = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constant.horse))
        {
            Stop = false;
        }
    }
}
