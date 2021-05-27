using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosBlack : MonoBehaviour
{
    public float _radius = 0.1f;

    private void OnDrawGizmos()
    {
        //����� ���� ����
        Gizmos.color = Color.black;
        //��ü ����� ����� ����. ���ڴ� (���� ��ġ, ������)
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
