using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
	public float hp = 2f;
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
		isLive = false;
		anim.SetBool("isDead", true);
		spriter.DOFade(0, 0.5f).OnComplete(() => { Destroy(this.gameObject); });
	}

	public float speed;
    public Rigidbody2D target;
	public float detectionRadius = 5.0f;
	public bool isLive;
	Rigidbody2D rigid;
    SpriteRenderer spriter;
	Material mat;
	Animator anim;
	float currSpeed;

	private void Awake()
	{
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
		mat = GetComponent<Renderer>().material;
		isLive = true;
	}

	private void Start()
	{
		target = GameManager.instance.player.GetComponent<Rigidbody2D>();
	}


	private void FixedUpdate()
	{
		if (!isLive) return;

		Vector2 dirVec = target.position - rigid.position;

		// Check if target is within detection radius
		if (dirVec.magnitude <= detectionRadius)
		{
			Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
			rigid.MovePosition(rigid.position + nextVec);
			rigid.velocity = Vector2.zero;
			currSpeed = nextVec.magnitude;
		}
		else
		{
			// If target is outside detection radius, stop movement
			currSpeed = 0;
		}
	}


	private void LateUpdate()
	{
		if (!isLive) return;

		anim.SetFloat("Speed", currSpeed);
		spriter.flipX = target.position.x < rigid.position.x;
	}

	private void OnTriggerEnter2D(Collider2D coll)
	{
		if (!isLive) return;

		if (coll.CompareTag("Bullet"))
		{
			var dmg = coll.GetComponent<Bullet>().damage;
			HP -= dmg;
			StartCoroutine(PlayHitEffect());
		}
	}

	IEnumerator PlayHitEffect()
	{
		mat.EnableKeyword("HITEFFECT_ON");
		yield return new WaitForSeconds(0.1f);
		mat.DisableKeyword("HITEFFECT_ON");
	}


}
