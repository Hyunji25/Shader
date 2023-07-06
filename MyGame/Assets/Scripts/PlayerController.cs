using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float PlayerSpeed = 5;
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
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(hor, 0, ver);
        dir.Normalize();

        dir = Camera.main.transform.TransformDirection(dir);

        yVelocity += gravity * Time.deltaTime;
        dir.y = yVelocity;

        cc.Move(dir * PlayerSpeed * Time.deltaTime);
    }
}
