using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 3;
    float dashspeed = 2;

    public float gravity = -20;
    float yVelocity;

    CharacterController cc;

    private void Awake()
    {
        cc = gameObject.GetComponent<CharacterController>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        float hAxis = Input.GetAxis("Horizontal");
        float vAxis = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(hAxis, 0, vAxis);
        dir.Normalize();

        dir = Camera.main.transform.TransformDirection(dir);

        if (Input.GetKey(KeyCode.LeftShift))
            transform.position += dir * speed * dashspeed * Time.deltaTime;
        else
            transform.position += dir * speed * Time.deltaTime;

        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        cc.Move(dir * speed * Time.deltaTime);
    }
}
