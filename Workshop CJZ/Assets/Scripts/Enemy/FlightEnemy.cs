using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightEnemy : MonoBehaviour
{
    public int health;
    public int damage;

    public float speed;
    float initialspeed;
    public float stopDistance;
    public bool isRight;

    public Rigidbody2D rig;
    public Animator anim;

    bool isDead;

    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        initialspeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        float playerPos = transform.position.x - player.position.x;

        if (playerPos > 0)
        {
            isRight = false;
        }
        else {
            isRight = true;
        }
        if (distance <= stopDistance) {
            speed = 0f;
            player.GetComponent<Player>().OnHit(damage);
        }
        else
        {
            speed = initialspeed;
        }
    }

    void FixedUpdate()
    {
        if (isDead == false) {
            if (isRight)
            {
                rig.velocity = new Vector2(speed, rig.velocity.y);
                transform.eulerAngles = new Vector2(0, 0);
            }
            else
            {
                rig.velocity = new Vector2(-speed, rig.velocity.y);
                transform.eulerAngles = new Vector2(0, 180);
            }
        }
        
    }

    public void OnHit() {
        

        if (health <= 0)
        {
            isDead = true;
            speed = 0f;
            anim.SetTrigger("death");
            Destroy(gameObject, 1f);
        }
        else {
            anim.SetTrigger("hit");
            health--;
        }
    }
}
