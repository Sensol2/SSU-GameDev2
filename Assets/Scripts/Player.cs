using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float initialSpeed = 1.5f;
    public float speed = 1.5f;
    public bool isFliped;
    public RuntimeAnimatorController[] animCon;
    public Scanner scanner;
    public Weapon weapon;

    public float hp = 100f;
    public float mp = 300f;
    public bool isInvincible;
    // 대쉬 관련 변수 추가
    public float dashDistance = 1f;
    public float dashTime = 0.2f;
    private float lastDash = -10.0f;
    public float dashCoolDown = 0.5f;

    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator anim;
    Material mat;

    public float HP
    {
        get
        {
            return hp;
        }
        set
        {
            hp = value;

            if (hp <= 0)
            {
                hp = 0; // Ensure HP doesn't go below zero
                Dead();
            }
        }
    }

    private void Dead()
    {
        spriter.DOFade(0, 0.5f).OnComplete(() => { Destroy(this.gameObject); });
    }

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
            speed = initialSpeed * 0.5f;
            spriter.flipX = scanner.nearestTarget.position.x < this.transform.position.x;
            isFliped = scanner.nearestTarget.position.x < this.transform.position.x;
        }
        else
        {
            speed = initialSpeed;
            Vector2 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            spriter.flipX = mousePos.x < this.transform.position.x;
            isFliped = mousePos.x < this.transform.position.x;
        }
    }

	private void OnCollisionEnter2D(Collision2D coll)
	{
        if (isInvincible) return;

        if (coll.gameObject.CompareTag("Enemy"))
        {
            isInvincible = true;
            this.HP -= coll.gameObject.GetComponent<Enemy>().damage;
            StartCoroutine(PlayHitEffect());
        }
    }


    // 대쉬 기능 구현
    void Dash()
    {
        mat.EnableKeyword("SHADOW_ON");
        Vector3 dashPosition = transform.position + new Vector3(inputVec.x, inputVec.y, 0) * dashDistance;
        transform.DOMove(dashPosition, dashTime).OnComplete(()=> { mat.DisableKeyword("SHADOW_ON"); });
    }

    IEnumerator PlayHitEffect()
    {
        mat.EnableKeyword("HITEFFECT_ON");
        yield return new WaitForSeconds(0.1f);
        mat.DisableKeyword("HITEFFECT_ON");
        yield return new WaitForSeconds(0.1f);
        mat.EnableKeyword("HITEFFECT_ON");
        yield return new WaitForSeconds(0.1f);
        mat.DisableKeyword("HITEFFECT_ON");
        yield return new WaitForSeconds(0.1f);
        mat.EnableKeyword("HITEFFECT_ON");
        yield return new WaitForSeconds(0.1f);
        mat.DisableKeyword("HITEFFECT_ON");
        isInvincible = false;
    }
}
