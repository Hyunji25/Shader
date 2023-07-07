using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int EHp;

    [SerializeField]
    private float ESpeed;

    [SerializeField]
    private float runSpeed;

    private float applySpeed;

    private Vector3 direction;

    private bool isAction;  // 행동 중인지 아닌지 판별
    private bool isWalking; // 걷는지, 안 걷는지 판별
    private bool isRunning; // 달리는지 판별
    private bool isDead;   // 죽었는지 판별

    // [애니메이션]
    //[SerializeField] 
    //private Animator anim;

    [SerializeField]
    private float walkTime;  // 걷는 시간

    [SerializeField]
    private float waitTime;  // 대기 시간

    [SerializeField]
    private float runTime;  // 뛰기 시간

    private float currentTime;

    [SerializeField]
    private Rigidbody rigid;

    [SerializeField]
    private BoxCollider boxCol;

    private void Awake()
    {
        currentTime = waitTime;
        isAction = true;

        rigid = GameObject.Find("Enemy").GetComponent<Rigidbody>();
        boxCol = GameObject.Find("Enemy").GetComponent<BoxCollider>();
    }

    void Start()
    {

    }

    void Update()
    {
        if (!isDead)
        {
            Move();
            Rotation();
            ElapseTime();
        }
    }

    private void Move()
    {
        if (isWalking || isRunning)
            rigid.MovePosition(transform.position + transform.forward * applySpeed * Time.deltaTime);
    }

    private void Rotation()
    {
        if (isWalking || isRunning)
        {
            Vector3 rotation = Vector3.Lerp(transform.eulerAngles, new Vector3(0f, direction.y, 0f), 0.01f);
            rigid.MoveRotation(Quaternion.Euler(rotation));
        }
    }

    private void ElapseTime()
    {
        if (isAction)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)  // 랜덤하게 다음 행동을 개시
                ReSet();
        }
    }

    private void ReSet()  // 다음 행동 준비
    {
        isWalking = false;
        isAction = true;
        //anim.SetBool("Walking", isWalking);
        isRunning = false;
        //anim.SetBool("Running", isRunning);
        applySpeed = ESpeed;

        direction.Set(0f, Random.Range(0f, 360f), 0f);

        //RandomAction();
        StartCoroutine(RandomAction());

    }

    private IEnumerator RandomAction()
    {
        //int _random = Random.Range(0, 3); // 대기, 두리번, 걷기
        int _random = 2; // 대기, 두리번, 걷기

        if (_random == 0)
            Wait();
        else if (_random == 1)
            Peek();
        else if (_random == 2)
            TryWalk();

        yield return new WaitForSeconds(10f);
    }

    /*
    private void RandomAction()
    {
        int _random = Random.Range(0, 3); // 대기, 두리번, 걷기

        if (_random == 0)
            Wait();
        else if (_random == 1)
            Peek();
        else if (_random == 2)
            TryWalk();
    }
     */

    private void Wait()  // 대기
    {
        currentTime = waitTime;
        Debug.Log("대기");
    }

    private void Peek()  // 두리번
    {
        currentTime = waitTime;
        //anim.SetTrigger("Peek");
        Debug.Log("두리번");
    }

    private void TryWalk()  // 걷기
    {
        currentTime = walkTime;
        isWalking = true;
        //anim.SetBool("Walking", isWalking);
        applySpeed = ESpeed;
        transform.position -= new Vector3(1.0f, 0.0f, 0.0f) * Time.deltaTime;
        Debug.Log("걷기");
    }

    public void Run(Vector3 targetPos)
    {
        direction = Quaternion.LookRotation(transform.position - targetPos).eulerAngles;

        currentTime = runTime;
        isWalking = false;
        isRunning = true;
        applySpeed = runSpeed;

        //anim.SetBool("Running", isRunning);
    }
}
