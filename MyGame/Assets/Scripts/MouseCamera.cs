using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCamera : MonoBehaviour
{
    public float speed = 200;

    float mx; 
    float my; 

    void Start()
    {
        
    }

    void Update()
    {
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");

        mx += h * speed * Time.deltaTime;
        my += v * speed * Time.deltaTime;

        my = Mathf.Clamp(my, -90, 90);

        transform.eulerAngles = new Vector3(-my, mx, 0);
    }
}
