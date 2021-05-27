using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// �� ���� RoadPos������Ʈ���� �����ϴ� Ŭ����
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
        if (currentMoveCar == null)         //���� �̵����� ���� ������
        {
            if (trafficQueue.Count > 0 && pedestrianWaiting == false && pedestrianWalking == false)     //�� �ڵ����� ������ �̵���Ŵ
            {
                currentMoveCar = trafficQueue.Dequeue();
                currentMoveCar.Stop = false;
            }
            else if (pedestrianWalking || pedestrianWaiting)   //��ٸ������̰ų� �ȴ����̸� �Բ� �ȴ´�.    MoveCrossŬ������ MovePedestrians
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
