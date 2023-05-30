using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    Player player;
	Rigidbody2D rigid;
	SpriteRenderer spriter;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }
    private void Start()
	{
		player = GameManager.instance.player;
	}

    private void FixedUpdate()
    {
        if (player.scanner.nearestTarget != null)
        {
            Vector2 targetPos = player.scanner.nearestTarget.transform.position;
            Vector2 myPos = transform.position;

            // Get direction to target
            Vector2 direction = targetPos - myPos;

            // Calculate angle
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Adjust for flipX
            if (player.isFliped)
            {
                angle = (angle > 0) ? angle - 180 : angle + 180;
            }

            // Rotate towards the target
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else 
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        
    }

    private void LateUpdate()
    {
        spriter.flipX = player.isFliped;
    }

}
