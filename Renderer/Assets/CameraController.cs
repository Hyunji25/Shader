using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera camera;
    private Vector3 direction;
    private Transform target;
    private float distance;

    public LayerMask mask;

    private const string path = "Custom/Transparent/MyShader";

    private bool Check;

    private void Awake()
    {
        //camera = GetComponent<Camera>();
        camera = Camera.main;

        target = GameObject.Find("Player").transform;
    }

    private void Start()
    {
        direction = (target.position - transform.position).normalized;

        distance = Vector3.Distance(target.position, transform.position);

        Check = false;
    }

    void Update()
    {
        Debug.DrawLine(transform.position, transform.position + direction * distance, Color.green);
        Ray ray = new Ray(transform.position, direction);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distance, mask))
        {
            if (hit.transform != null)
            {
                if (Check == false)
                {
                    Renderer renderer = hit.transform.GetComponent<Renderer>();
                    Check = true;
                    if (renderer != null)
                        StartCoroutine(SetColor(renderer));
                }
                else // Check == true
                {
                    Renderer renderer = hit.transform.GetComponent<Renderer>();
                    Check = false;
                    if (renderer != null)
                        StartCoroutine(SetOrigin(renderer));
                }

                Debug.Log(Check);
            }
        }
    }

    IEnumerator SetColor(Renderer renderer)
    {
        // ** Color값 변경이 가능한 Shader로 변경
        Material material = new Material(Shader.Find(path));

        // ** 변경된 Shader의 Color값을 받아옴
        Color color = renderer.material.color;

        // ** color.a이 0.5f보다 큰 경우에만 반복
        while (0.5f < color.a)
        {
            yield return null;

            // ** Alpha(1) -= Time.deltaTime
            color.a -= Time.deltaTime;

            renderer.material.color = color;
        }
    }

    IEnumerator SetOrigin(Renderer renderer)
    {
        // ** Color값 변경이 가능한 Shader로 변경
        Material material = new Material(Shader.Find(path));

        // ** 변경된 Shader의 Color값을 받아옴
        Color color = renderer.material.color;

        // ** color.a이 1.0f보다 작은 경우에만 반복
        while (color.a < 1.0f)
        {
            yield return null;

            // ** Alpha(1) -= Time.deltaTime
            color.a += Time.deltaTime;

            renderer.material.color = color;
        }
    }
}
