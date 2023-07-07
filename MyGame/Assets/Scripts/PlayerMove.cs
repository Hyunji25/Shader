using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float PSpeed = 10;

    [SerializeField]
    private float lookSensitivity = 2;

    [SerializeField]
    private float cameraRotationLimit = 45;

    private float currentCameraRotationX;

    [SerializeField]
    private Camera myCamera;

    private Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();

        myCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        Move();
        CameraRotation();
        CharacterRotation();
    }

    private void Move()
    {
        float yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, yRotation, 0f) * lookSensitivity;
        rigid.MoveRotation(rigid.rotation * Quaternion.Euler(_characterRotationY));
    }

    private void CameraRotation()
    {
        float xRotation = Input.GetAxisRaw("Mouse Y");
        float cameraRotationX = xRotation * lookSensitivity;

        currentCameraRotationX -= cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        myCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }

    private void CharacterRotation()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        Vector3 moveHorizontal = transform.right * hor;
        Vector3 moveVertical = transform.forward * ver;

        Vector3 velocity = (moveHorizontal + moveVertical).normalized * PSpeed; // ม฿ทย

        rigid.MovePosition(transform.position + velocity * Time.deltaTime);
    }
}
