using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// 건물의 부모스크립트
/// </summary>
public class Building : XRBaseInteractable, IInteractive
{
    public HorsePoint fromPoint;
    public HorsePoint toPoint;
    public virtual void Interactive()
    {

    }

    public virtual void StopInteractive()
    {
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        base.OnHoverEntered(args);
    }

}
