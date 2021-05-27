using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class PlayerRay : XRRayInteractor
{
    private Vector3 position;
    private Vector3 normal;

    private int one;
    private int two;
    private bool check;
    private void Update()
    {
        TryGetHitInfo(out position, out normal, out one, out check);
        print(position);
        print(normal);
        print(one);
        print(check);
    }
}
