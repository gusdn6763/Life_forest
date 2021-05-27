using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// �⺻������ IInteractive�� ��ӹ��� Ŭ������ ��ȣ�ۿ��� �����ϰ� ������ ����
/// ex) �ǹ� Ŭ���� �ǹ� ���ų�, �ڵ��� Ŭ���� �ڵ��� ���ų�, ���� Ŭ���� ���ó�� ������ ���ٰų�
/// </summary>
public interface IInteractive
{
    void Interactive();
    void StopInteractive();
}
