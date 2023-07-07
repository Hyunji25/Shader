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

    private bool isAction;  // �ൿ ������ �ƴ��� �Ǻ�
    private bool isWalking; // �ȴ���, �� �ȴ��� �Ǻ�
    private bool isRunning; // �޸����� �Ǻ�
    private bool isDead;   // �׾����� �Ǻ�

    // [�ִϸ��̼�]
    //[SerializeField] 
    //private Animator anim;

    [SerializeField]
    private float walkTime;  // �ȴ� �ð�

    [SerializeField]
    private float waitTime;  // ��� �ð�

    [SerializeField]
    private float runTime;  // �ٱ� �ð�

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
            if (currentTime <= 0)  // �����ϰ� ���� �ൿ�� ����
                ReSet();
        }
    }

    private void ReSet()  // ���� �ൿ �غ�
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
        //int _random = Random.Range(0, 3); // ���, �θ���, �ȱ�
        int _random = 2; // ���, �θ���, �ȱ�

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
        int _random = Random.Range(0, 3); // ���, �θ���, �ȱ�

        if (_random == 0)
            Wait();
        else if (_random == 1)
            Peek();
        else if (_random == 2)
            TryWalk();
    }
     */

    private void Wait()  // ���
    {
        currentTime = waitTime;
        Debug.Log("���");
    }

    private void Peek()  // �θ���
    {
        currentTime = waitTime;
        //anim.SetTrigger("Peek");
        Debug.Log("�θ���");
    }

    private void TryWalk()  // �ȱ�
    {
        currentTime = walkTime;
        isWalking = true;
        //anim.SetBool("Walking", isWalking);
        applySpeed = ESpeed;
        transform.position -= new Vector3(1.0f, 0.0f, 0.0f) * Time.deltaTime;
        Debug.Log("�ȱ�");
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
