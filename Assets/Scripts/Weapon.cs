using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public float speed;
    public GameObject bullet;

    float timer;
    Player player;

    private void Start()
    {
        player = GameManager.instance.player;
    }

    void Update()
    {
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime); //Update에서 이동,회전을 다룰때는 deltaTime 곱해주기
                break;
            default:
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
        Debug.Log("Fire");
        if (player.scanner.nearestTarget == null) return;

        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        GameObject obj = Instantiate(bullet, transform.position, Quaternion.FromToRotation(Vector3.up, dir));
        obj.GetComponent<Bullet>().Init(damage, dir);

        //AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);
    }
}
