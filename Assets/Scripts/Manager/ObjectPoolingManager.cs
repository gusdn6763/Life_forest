using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    [SerializeField] private List<CloudMove> CloudPrefabs;                      // 클라우드 프리팹

    [SerializeField] private float spawnTime;
}
