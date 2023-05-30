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
        inputVec.x = Input.GetAxisRaw("Horizontal"); //Raw로 명확한 컨트롤 구현
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    //물리에 대해서는 FixedUpadate로 이용
    private void FixedUpdate()
    {
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    private void LateUpdate() //프레임이 종료 되기 전 실행되는 생명주기 함수
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
