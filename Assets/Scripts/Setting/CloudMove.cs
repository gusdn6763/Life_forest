using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    // ���� �ӵ� ����
    public float speed = 30.0f;

    private void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("DestroyBlock"))
        {
            gameObject.SetActive(false);
        }
    }
}
