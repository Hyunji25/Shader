using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotation : MonoBehaviour
{
    public float rotSpeed = 500;

    float mx;
    float my;

    void Start()
    {
        
    }

    void Update()
    {
        float hor = Input.GetAxis("Mouse X");
        float ver = Input.GetAxis("Mouse Y");

        mx += hor * rotSpeed * Time.deltaTime;
        my += ver * rotSpeed * Time.deltaTime;

        my = Mathf.Clamp(my, -90, 90);

        transform.eulerAngles = new Vector3(-my, mx, 0);
    }
}
