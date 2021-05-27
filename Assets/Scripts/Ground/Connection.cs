using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Connection : MonoBehaviour
{
    [SerializeField] protected RayPoint rayPoint = RayPoint.NONE;
    [SerializeField] protected float rayLength;
    protected Vector3 rayDir;

    public virtual void FindAndConnection()
    {
        //레이를 쏨
        switch (rayPoint)
        {
            case RayPoint.LEFT:
                rayDir = -transform.forward;
                break;
            case RayPoint.RIGHT:
                rayDir = transform.forward;
                break;
            case RayPoint.TOP:
                rayDir = -transform.right;
                break;
            case RayPoint.BOTTOM:
                rayDir = transform.right;
                break;
        }

    }

    /// <summary>
    /// 디버깅용 및 확인용
    /// </summary>
    private void OnDrawGizmos()
    {
        switch (rayPoint)
        {
            case RayPoint.LEFT:
                rayDir = -transform.forward;
                break;
            case RayPoint.RIGHT:
                rayDir = transform.forward;
                break;
            case RayPoint.TOP:
                rayDir = -transform.right;
                break;
            case RayPoint.BOTTOM:
                rayDir = transform.right;
                break;
        }

        RaycastHit hit;
        if (Physics.SphereCast(transform.position, 1f, rayDir * rayLength, out hit, rayLength, 1 << 3))
        {
        }
        else
        {
            Gizmos.DrawRay(transform.position, rayDir * rayLength);
        }
    }
}
