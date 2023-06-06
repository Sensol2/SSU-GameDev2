using Com.LuisPedroFonseca.ProCamera2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ExplosiveBox : MonoBehaviour
{
    public LayerMask targetLayer;
    ProCamera2DShake proCamera2DShake;
    [SerializeField]
    public GameObject effect;
    private void Start()
    {
        proCamera2DShake = Camera.main.GetComponent<ProCamera2DShake>();
    }
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Bullet"))
        {
            proCamera2DShake.Shake(1f, Vector2.one);
            ExplosionDamage();
            PlayVFX();
            Destroy(this.gameObject);
        }
    }

    private void PlayVFX()
    {
        Instantiate(effect, transform.position, Quaternion.identity);
    }

    public void ExplosionDamage()
    {
        RaycastHit2D[] targets;
        targets = Physics2D.CircleCastAll(transform.position, 3.0f, Vector2.zero, 0, targetLayer);
        foreach (RaycastHit2D target in targets)
        {
            Debug.Log(target.transform.name);
            target.transform.GetComponent<Enemy>().HP -= 30;
        }
    }
}
