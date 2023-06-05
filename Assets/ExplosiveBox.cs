using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBox : MonoBehaviour
{
    [SerializeField]
    public GameObject effect;


    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Bullet"))
        {
            PlayVFX();
            Destroy(this.gameObject);
        }
    }

    private void PlayVFX()
    {
        Instantiate(effect, transform.position, Quaternion.identity);
    }
}
