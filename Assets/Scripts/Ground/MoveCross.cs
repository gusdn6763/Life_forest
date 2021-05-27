
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveCross : MonoBehaviour
{
    List<People> peoples = new List<People>();

    [field: SerializeField] public UnityEvent OnPedestrianEnter { get; set; }

    [field: SerializeField] public UnityEvent OnPedestrianExit { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constant.people))
        {
            People pedestrian = other.GetComponent<People>();
            if (peoples.Contains(pedestrian) == false)          //포함되어 있지 않으면 추가
            {
                peoples.Add(pedestrian);
                pedestrian.Stop = true;                         //일단 멈춤
                OnPedestrianEnter?.Invoke();                    //인스펙터의 OnPedestrianEnter실행
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constant.people))
        {
            People pedestrian = other.GetComponent<People>();
            if (pedestrian != null)
            {
                RemovePedestrian(pedestrian);
            }
        }
    }

    private void RemovePedestrian(People pedestrian)
    {
        peoples.Remove(pedestrian);
        if (peoples.Count <= 0)
            OnPedestrianExit?.Invoke();
    }

    /// <summary>
    /// Road클래스의 OnPedestrianCanWalk
    /// </summary>
    public void MovePedestrians()
    {
        foreach (var pedestrian in peoples)
        {
            pedestrian.Stop = false;
        }
    }
}
