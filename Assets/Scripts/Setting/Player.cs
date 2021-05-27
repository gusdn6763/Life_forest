using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Player : MonoBehaviour
{
    public static Player instance;

    [SerializeField] protected float speed;         //�÷��̾� �ӵ�
    [SerializeField] private XRNode playerMoveDevice;                //��� ���� �̵����� ���ϴ� ����

    private CharacterController characterController;     //VR Rig�� ĳ���� ��Ʈ�ѷ�
    private XRRig rig;                          

    public XRController climbingHand;
    private Vector2 inputAxis;
    public float mass = 1f;                                     //����޴� �߷�ũ��
    public float additionalHeight = 0.2f;                       //�߰����� �Ӹ� ũ��
    public bool moveImpossible = false;                         //�÷��̾� �̵��� ����

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        rig = GetComponent<XRRig>();
        characterController = GetComponent<CharacterController>();
    }



    private void Update()
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(playerMoveDevice);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
    }

    private void FixedUpdate()
    {
        CapsuleFollowHeadset();

        if (!moveImpossible)
        {
            StartMove();
            ApplyGravity();
        }
        else
        {
            ApplyGravity();
        }
        
    }

    /// <summary>
    /// ����� ���̰��� ���� PlayerController height�� ����
    /// </summary>
    void CapsuleFollowHeadset()
    {
        characterController.height = rig.cameraInRigSpaceHeight + additionalHeight;
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.cameraGameObject.transform.position);
        characterController.center = new Vector3(capsuleCenter.x, characterController.height / 2 + characterController.skinWidth + additionalHeight, capsuleCenter.z);
    }


    //�Է¹��� ���̽�ƽ������ �̵�
    void StartMove()
    {
        Quaternion headYaw = Quaternion.Euler(0, rig.cameraGameObject.transform.eulerAngles.y, 0);
        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);

        characterController.Move(direction * Time.fixedDeltaTime * speed);
    }

    //�߷� ����
    void ApplyGravity()
    {
        Vector3 gravity = new Vector3(0, Physics.gravity.y * mass, 0);
        gravity.y *= Time.deltaTime;
        characterController.Move(gravity * Time.deltaTime);
    }

}

