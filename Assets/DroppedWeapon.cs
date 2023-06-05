using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedWeapon : MonoBehaviour
{
    [SerializeField]
    private WeaponData weaponData;
    SpriteRenderer spriter;

    private void Start()
	{
        spriter = GetComponent<SpriteRenderer>();
        spriter.sprite = weaponData.sprite;
    }

	private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.player.weapon.ChangeWeapon(weaponData.id);
            Destroy(gameObject);
        }
    }

}
