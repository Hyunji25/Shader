using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCamera : MonoBehaviour
{
    private float CameraSpeed = 350;

    private float mx;
    private float my; 

    void Start()
    {
        
    }

    void Update()
    {
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");

        mx += h * CameraSpeed * Time.deltaTime;
        my += v * CameraSpeed * Time.deltaTime;

        my = Mathf.Clamp(my, -40, 10);

        transform.eulerAngles = new Vector3(-my, mx, 0);
    }
}
