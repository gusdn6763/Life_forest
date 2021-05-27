using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// 한 도로 RoadPos오브젝트들을 관리하는 클래스
/// </summary>
public class Road : Ground
{
    [field: SerializeField] public UnityEvent OnPedestrianCanWalk { get; set; }

    [SerializeField] private bool pedestrianWaiting = false;
    [SerializeField] private bool pedestrianWalking = false;
    [SerializeField] private bool newLand = false;

    private Queue<Horse> trafficQueue = new Queue<Horse>();
    private HorseMove[] roadMoves;
    private Horse currentMoveCar;



    protected override void Awake()
    {
        base.Awake();
        roadMoves = GetComponentsInChildren<HorseMove>();
    }

    protected void Start()
    {
        if (newLand)
        {
            for(int i = 0; i < roadMoves.Length; i++)
            {
                roadMoves[i].FindAndConnection();
            }
        }
    }

    private void Update()
    {
        if (currentMoveCar == null)         //현재 이동중인 차가 없으면
        {
            if (trafficQueue.Count > 0 && pedestrianWaiting == false && pedestrianWalking == false)     //그 자동차를 빼내서 이동시킴
            {
                currentMoveCar = trafficQueue.Dequeue();
                currentMoveCar.Stop = false;
            }
            else if (pedestrianWalking || pedestrianWaiting)   //기다리는중이거나 걷는중이면 함께 걷는다.    MoveCross클래스의 MovePedestrians
            {
                OnPedestrianCanWalk?.Invoke();
                pedestrianWalking = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.horse))
        {
            Horse car = other.GetComponent<Horse>();
            if (car != currentMoveCar)
            {
                trafficQueue.Enqueue(car);
                car.Stop = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constant.horse))
        {
            Horse car = other.GetComponent<Horse>();
            if (car == currentMoveCar)
            {
                currentMoveCar = null;
            }
        }
    }

    public void SetPedestrianFlag(bool val)
    {
        if (val)
        {
            pedestrianWaiting = true;
        }
        else
        {
            pedestrianWaiting = false;
            pedestrianWalking = false;
        }
    }
}
