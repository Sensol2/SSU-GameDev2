using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Bullet : MonoBehaviour
{
    public float damage;

    [SerializeField]
    public GameObject effect;

    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, float bulletSpeed, Vector3 dir)
    {
        this.damage = damage;
        rigid.velocity = dir * bulletSpeed;
    }

	private void OnTriggerEnter2D(Collider2D coll)
	{
        if (coll.CompareTag("Enemy") || coll.CompareTag("Wall"))
        {
            rigid.velocity = Vector2.zero;
            PlayVFX();
            Destroy(this.gameObject);
        }
    }

    private void PlayVFX()
    {
        Instantiate(effect, transform.position, Quaternion.identity);
    }

}
