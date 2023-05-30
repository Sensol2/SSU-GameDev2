using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed = 1.5f;
    public bool isFliped;
    public RuntimeAnimatorController[] animCon;
    public Scanner scanner;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
    }

    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal"); //Raw�� ��Ȯ�� ��Ʈ�� ����
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    //������ ���ؼ��� FixedUpadate�� �̿�
    private void FixedUpdate()
    {
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    private void LateUpdate() //�������� ���� �Ǳ� �� ����Ǵ� �����ֱ� �Լ�
    {
        anim.SetFloat("Speed", inputVec.magnitude);


        if (scanner.nearestTarget != null)
        {
            spriter.flipX = scanner.nearestTarget.position.x < this.transform.position.x;
            isFliped = scanner.nearestTarget.position.x < this.transform.position.x;
        }
        else if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
            isFliped = inputVec.x < 0;
        }
    }
}
