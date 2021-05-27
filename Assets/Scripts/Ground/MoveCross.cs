
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
            if (peoples.Contains(pedestrian) == false)          //���ԵǾ� ���� ������ �߰�
            {
                peoples.Add(pedestrian);
                pedestrian.Stop = true;                         //�ϴ� ����
                OnPedestrianEnter?.Invoke();                    //�ν������� OnPedestrianEnter����
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
    /// RoadŬ������ OnPedestrianCanWalk
    /// </summary>
    public void MovePedestrians()
    {
        foreach (var pedestrian in peoples)
        {
            pedestrian.Stop = false;
        }
    }
}
