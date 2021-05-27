using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// 기본적으로 IInteractive를 상속받은 클래스만 상호작용이 가능하게 구현할 예정
/// ex) 건물 클릭시 건물 보거나, 자동차 클릭시 자동차 보거나, 지형 클릭시 사람처럼 지형을 본다거나
/// </summary>
public interface IInteractive
{
    void Interactive();
    void StopInteractive();
}
