using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed = 1.5f;
    public bool isFliped;
    public RuntimeAnimatorController[] animCon;
    public Scanner scanner;
    public Weapon weapon;

    // 대쉬 관련 변수 추가
    public float dashDistance = 1f;
    public float dashTime = 0.2f;
    private float lastDash = -10.0f;
    public float dashCoolDown = 0.5f;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;
    Material mat;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
        mat = GetComponent<Renderer>().material;
        weapon = GetComponentInChildren<Weapon>();
    }

    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");

        // 대쉬 입력 확인 및 실행
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Dash();
            lastDash = Time.time;
        }
    }

    private void FixedUpdate()
    {
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    private void LateUpdate()
    {
        anim.SetFloat("Speed", inputVec.magnitude);

        if (scanner.nearestTarget != null && Input.GetMouseButton(0))
        {
            spriter.flipX = scanner.nearestTarget.position.x < this.transform.position.x;
            isFliped = scanner.nearestTarget.position.x < this.transform.position.x;
        }
        else
        {
            Vector2 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            spriter.flipX = mousePos.x < this.transform.position.x;
            isFliped = mousePos.x < this.transform.position.x;
        }
    }

    // 대쉬 기능 구현
    void Dash()
    {
        mat.EnableKeyword("SHADOW_ON");
        Vector3 dashPosition = transform.position + new Vector3(inputVec.x, inputVec.y, 0) * dashDistance;
        transform.DOMove(dashPosition, dashTime).OnComplete(()=> { mat.DisableKeyword("SHADOW_ON"); });
    }
}
