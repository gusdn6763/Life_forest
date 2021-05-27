using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Horse : MonoBehaviour
{
    [SerializeField] private float collisionRaycastLength;
    [SerializeField] private float speed = 8f;                //�ӵ�

    private HorseMove currentPoint;           //���� ���� -> ��ǥ ����
    private HorseMove nextPoint;              //��ǥ ����
    private bool stop;                      //������ �浹, ���߱����� stop

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
    /// ���̵�
    /// </summary>
    public void MoveHorse()
    {
        int currentCount = Random.Range(0, currentPoint.wayPoints.Count);
        HorsePoint nextWayPoint = currentPoint.wayPoints[currentCount];

        transform.DOPath(nextWayPoint.toPoint, speed, nextWayPoint.PathType).
        SetLookAt(0.1f).SetEase(Ease.Linear).SetSpeedBased(true).OnComplete(() =>
        {
            nextPoint = nextWayPoint.trans[0].GetComponent<HorseMove>();     //���� ��ġ���� ����

            currentPoint = nextPoint;       //���� ���� = ��ǥ����
            if (currentPoint.wayPoints.Count == 0)      //�ν����Ϳ��� ���� �� �־���ų� ���� ������ �̵��Ұ��
            {
                print(currentPoint.transform.gameObject);
                print("���� ����");
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
