using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class People : MonoBehaviour
{
    Animator animator;
    private Marker currentPoint;           //현재 지점 -> 목표 지점
    private Marker nextPoint;              //목표 지점
    public float speed;
    public Queue<Marker> movePoints;

    private bool stop;
    public bool Stop
    {
        get { return stop; }
        set
        {
            stop = value;
            if (stop)
            {
                print("멈춤");
                //animator.SetBool("Walk", false);
            }
            else
            {
                //animator.SetBool("Walk", true);
            }
        }
    }

    public void PeopleActive(Marker currentPoint)
    {
        this.currentPoint = currentPoint;
        MovePeople();
    }


    /// <summary>
    /// 사람AI가 회사를 찾아 갈수 있도록 설정
    /// </summary>
    /// <returns></returns>
    public void MovePeople()
    {
        movePoints.Enqueue(currentPoint);
        int currentCount = Random.Range(0, currentPoint.adjacentMarkers.Count);
        nextPoint = currentPoint.adjacentMarkers[currentCount];

        transform.DOMove(nextPoint.Position, speed).SetEase(Ease.Linear).SetSpeedBased(true).OnComplete(() =>
        {
            currentPoint = nextPoint;       //현재 지점 = 목표지점
            nextPoint = nextPoint.adjacentMarkers[Random.Range(0, currentPoint.adjacentMarkers.Count)];        

            if (nextPoint == null)      //인스펙터에서 값을 안 넣어줬거나 도로 끝으로 이동할경우
            {
                print(currentPoint.transform.gameObject);
                print("길이 막힘");
                Destroy(this.gameObject, 1f);
            }
            else
            {
                MovePeople();
            }
        });
    }

    public void GetHome()
    {
        DOTween.Pause(transform);
        nextPoint = movePoints.Dequeue();
        transform.DOMove(nextPoint.Position, speed).SetEase(Ease.Linear).SetSpeedBased(true).OnComplete(() =>
        {
            currentPoint = nextPoint;       //현재 지점 = 목표지점
            nextPoint = movePoints.Dequeue();

            if (nextPoint == null)      //인스펙터에서 값을 안 넣어줬거나 도로 끝으로 이동할경우
            {
                print(currentPoint.transform.gameObject);
                print("집에 도착");
                DOTween.Pause(transform);
                Destroy(this.gameObject, 1f);
            }
        });
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag(Constant.Terrain)
    }
}
