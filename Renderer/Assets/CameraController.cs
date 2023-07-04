using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CameraController : MonoBehaviour
{
    private Camera camera;
    private Vector3 direction;
    private Transform target;
    private float distance;

    public LayerMask mask;

    private const string path = "Custom/Transparent/MyShader";

    private bool Check;

    public List<Renderer> objectRenderers = new List<Renderer>();

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

        // ** 모든 충돌을 감지
        RaycastHit[] hits = Physics.RaycastAll(transform.position, direction, distance, mask);

        // ** 충돌된 모든 원소들 중에 Renderer만 추출한 새로운 리스트를 생성
        List<Renderer> renderers = hits.Select(hit => hit.transform.GetComponent<Renderer>()).Where(renderer => renderer != null).ToList();

        //renderers.Select(renderer => objectRenderers.Contains(renderer));

        // ** 기존 리스트에는 포함되었지만 현재 ray에 감지된 리스트에는 없는 Renderer
        List<Renderer> extractionList = objectRenderers.Where(renderer => !renderers.Contains(renderer)).ToList();

        foreach (Renderer renderer in renderers)
        {
            // ** 충돌이 있다면 Renderer를 확인
            if (renderer != null)
            {
                // ** List에 이미 포함된 Renderer인지 확인
                if (!objectRenderers.Contains(renderer))
                {
                    StartCoroutine(SetFadeOut(renderer));
                }
            }

            foreach (Renderer element in objectRenderers)
            {
                if (renderers.Contains(element))
                {
                    // ** 투명화된 객체를 원래 상태로 되돌림
                    StartCoroutine(SetFadeIn(renderer));
                }
            }
        }


        //hits.ToList().Contains();

        /*
        // ** hits 배열의 모든 원소를 확인
        foreach (RaycastHit hit in hits)
        {
            // ** ray의 충돌이 감지된 Object의 Renderer를 받아옴
            Renderer renderer = hit.transform.GetComponent<Renderer>();
            HitList.Add(renderer);

            // ** renderer == null 이라면 다음 원소를 확인
            if (renderer == null)
                continue;

            // ** 이전 리스트 중에 동일한 원소가 포함되어 있는지 확인
            if (!objectRenderers.Contains(renderer))
            {
                // ** 포함되지 않았다면...
                objectRenderers.Add(renderer); // ** 추가
            }

            foreach (Renderer element in objectRenderers)
            {
                if (HitList.Contains(element))
                {
                    // ** 투명화된 객체를 원래 상태로 되돌림
                    StartCoroutine(SetFadeIn(renderer));
                }
            }

            // ** 충돌이 있다면 Renderer를 확인
            if (renderer != null)
            {
                // ** List에 이미 포함된 Renderer인지 확인
                if (!objectRenderers.Contains(renderer))
                {
                    StartCoroutine(SetFadeOut(renderer));
                }
            }
        }

        // ** 확인된 모든 Renderer의 투명화 작업을 진행
        */
    }

    IEnumerator SetFadeOut(Renderer renderer)
    {
        objectRenderers.Add(renderer); // ** 추가

        // ** Color값 변경이 가능한 Shader로 변경
        renderer.material = new Material(Shader.Find(path));

        // ** 변경된 Shader의 Color값을 받아옴
        Color color = renderer.material.color;

        // ** color.a이 1.0f보다 작은 경우에만 반복
        while (0.3f < color.a)
        {
            yield return null;

            // ** Alpha(1) -= Time.deltaTime
            color.a -= Mathf.Clamp(Time.deltaTime * 10, 0.1f, 0.5f);

            renderer.material.color = color;
        }
    }

    IEnumerator SetFadeIn(Renderer renderer)
    {
        // ** Color값 변경이 가능한 Shader로 변경
        renderer.material = new Material(Shader.Find(path));

        // ** 변경된 Shader의 Color값을 받아옴
        Color color = renderer.material.color;

        // ** color.a이 1.0f보다 작은 경우에만 반복
        while (color.a < 1.0f)
        {
            yield return null;

            // ** Alpha(1) -= Time.deltaTime
            color.a += Mathf.Clamp(Time.deltaTime * 10, 0.1f, 0.5f);

            renderer.material.color = color;
        }
    }
}
