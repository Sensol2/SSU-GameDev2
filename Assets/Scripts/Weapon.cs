using Com.LuisPedroFonseca.ProCamera2D;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    ProCamera2DShake proCamera2DShake;
    [SerializeField]
    private List<WeaponData> weaponDatas;

    public int id;
    public float damage;
    public float speed;
    public float bulletSpeed;
    public GameObject bullet;

    float timer;
    Player player;
    SpriteRenderer spriter;

    private void Start()
    {
        proCamera2DShake = Camera.main.GetComponent<ProCamera2DShake>();
        player = GameManager.instance.player;
        spriter = GetComponent<SpriteRenderer>();
        ChangeWeapon(1);
    }

    public void ChangeWeapon(int id)
    {
        this.id = weaponDatas[id].id;
        this.damage = weaponDatas[id].damage;
        this.speed = weaponDatas[id].speed;
        this.bullet = weaponDatas[id].bullet;
        this.bulletSpeed = weaponDatas[id].bulletSpeed;
        this.spriter.sprite = weaponDatas[id].sprite;
    }

    void Update()
    {
        switch (id)
        {
            case 0: // 근접
                transform.Rotate(Vector3.back * speed * Time.deltaTime); //Update에서 이동,회전을 다룰때는 deltaTime 곱해주기
                break;
            case 3: // 샷건
                timer += Time.deltaTime;

                if (timer > speed)
                {
                    timer = 0f;
                    ShotgunFire();
                }
                break;
            default: //원거리
                timer += Time.deltaTime;

                if (timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;
        }
    }

    private void Fire()
    {
        if (!Input.GetMouseButton(0))
            return;


        Vector3 targetPos;

        if (player.scanner.nearestTarget != null)
        {
            targetPos = player.scanner.nearestTarget.position;
        }
        else
        {
            Vector2 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            targetPos = new Vector3(mousePos.x, mousePos.y, 0);
        }

        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 180;
        GameObject obj = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, angle));
        obj.GetComponent<Bullet>().Init(damage, bulletSpeed, dir);

        SoundManager.instance.PlayShootSound();
    }

    private void ShotgunFire()
    {
        if (!Input.GetMouseButton(0))
            return;

        proCamera2DShake.Shake(1f, Vector2.one);
        Vector3 targetPos;

        if (player.scanner.nearestTarget != null)
        {
            targetPos = player.scanner.nearestTarget.position;
        }
        else
        {
            Vector2 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            targetPos = new Vector3(mousePos.x, mousePos.y, 0);
        }

        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        // 5 bullets spread out in a fan-like shape
        for (int i = -3; i <= 3; i++)
        {
            float spreadAngle = angle + (i * 5.5f); // Each bullet is 22.5 degrees apart
            Quaternion bulletRotation = Quaternion.Euler(0, 0, spreadAngle);

            GameObject obj = Instantiate(bullet, transform.position, bulletRotation);
            Vector3 bulletDirection = bulletRotation * Vector3.right; // This rotates the direction vector
            obj.GetComponent<Bullet>().Init(damage, bulletSpeed, bulletDirection);
        }

        SoundManager.instance.PlayShootSound();
    }

}
