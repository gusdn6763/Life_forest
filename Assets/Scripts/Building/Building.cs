using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// �ǹ��� �θ�ũ��Ʈ
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
