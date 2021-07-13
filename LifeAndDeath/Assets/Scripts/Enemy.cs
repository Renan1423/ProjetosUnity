using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private Rigidbody2D rig;
    private SpriteRenderer sprite;
    public float speed;

    public Transform point;
    public float radius;

    public LayerMask wall;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        rig.velocity = new Vector2(speed, rig.velocity.y);

        CheckCollisionWithWall();
    }

    void CheckCollisionWithWall() {
        Collider2D hit = Physics2D.OverlapCircle(point.position, radius, wall);

        if (hit != null)
        {
            speed *= -1;

            float rotY = transform.position.y;

            if (transform.eulerAngles.y == 0)
            {
                rotY = 180;
            }
            else {
                rotY = 0;
            }
                transform.eulerAngles = new Vector3(0, rotY);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(point.position, radius);
    }


}
