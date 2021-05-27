using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 도로 끝자리에서 차량이 생성하도록 구현할 예정인 클래스
/// 현재 지정한 위치에서 자동차를 생성
/// </summary>
public class HorseMake : MonoBehaviour
{
    [SerializeField] private List<Horse> cars;
    [SerializeField] private HorseMove[] startPoint;
    [SerializeField] private float waitTime;
    [SerializeField] private float randomWaitTimeRange;
    
    private void Start()
    {
        StartCoroutine(MakeHorse());
    }

    IEnumerator MakeHorse()
    {
        int randomStartPoint = Random.Range(0, startPoint.Length);
        while (true)
        {
            if (startPoint[randomStartPoint].wayPoints.Count != 0)
            {
                Horse car = Instantiate(cars[Random.Range(0, cars.Count)], startPoint[randomStartPoint].transform.position, startPoint[randomStartPoint].transform.rotation);
                car.SetStartPosition(startPoint[randomStartPoint]);
            }
            randomStartPoint = Random.Range(0, startPoint.Length);
            yield return new WaitForSeconds(waitTime + Random.Range(0, randomWaitTimeRange));
        }
    }
}
