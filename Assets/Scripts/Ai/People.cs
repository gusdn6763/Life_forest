using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class People : MonoBehaviour
{
    Animator animator;
    private Marker currentPoint;           //���� ���� -> ��ǥ ����
    private Marker nextPoint;              //��ǥ ����
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
                print("����");
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
    /// ���AI�� ȸ�縦 ã�� ���� �ֵ��� ����
    /// </summary>
    /// <returns></returns>
    public void MovePeople()
    {
        movePoints.Enqueue(currentPoint);
        int currentCount = Random.Range(0, currentPoint.adjacentMarkers.Count);
        nextPoint = currentPoint.adjacentMarkers[currentCount];

        transform.DOMove(nextPoint.Position, speed).SetEase(Ease.Linear).SetSpeedBased(true).OnComplete(() =>
        {
            currentPoint = nextPoint;       //���� ���� = ��ǥ����
            nextPoint = nextPoint.adjacentMarkers[Random.Range(0, currentPoint.adjacentMarkers.Count)];        

            if (nextPoint == null)      //�ν����Ϳ��� ���� �� �־���ų� ���� ������ �̵��Ұ��
            {
                print(currentPoint.transform.gameObject);
                print("���� ����");
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
            currentPoint = nextPoint;       //���� ���� = ��ǥ����
            nextPoint = movePoints.Dequeue();

            if (nextPoint == null)      //�ν����Ϳ��� ���� �� �־���ų� ���� ������ �̵��Ұ��
            {
                print(currentPoint.transform.gameObject);
                print("���� ����");
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
