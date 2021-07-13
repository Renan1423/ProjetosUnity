using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int health;
    public int damage;

    public float speed;
    float initialspeed;
    public float stopDistance;
    public bool isRight;

    public Rigidbody2D rig;
    public Animator anim;

    Transform barrier;
    Transform barrierR;
    Transform player;

    private bool recovery;
    public float recoveryTime;
    private float recoveryCounter;

    public bool isRightEnemy;

    bool isDead;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        barrier = GameObject.FindGameObjectWithTag("LimiteLR").transform;
        barrierR = GameObject.FindGameObjectWithTag("LimitRight").transform;
        initialspeed = speed;
        recovery = true;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, barrier.position);
        float distanceR = Vector2.Distance(transform.position, barrierR.position);
        float barrierPos = transform.position.x - barrier.position.x;
        float barrierPosR = transform.position.x - barrierR.position.x;
        if (isRightEnemy) {
            if (barrierPosR > 0)
            {
                isRight = false;
            }
            else
            {
                isRight = true;
            }
        }
        else {
            if (barrierPos > 0)
            {
                isRight = false;
            }
            else
            {
                isRight = true;
            }
        }
        if (isRightEnemy)
        {
            if (distanceR >= stopDistance && recovery == false)
            {
                speed = 0f;
                anim.SetBool("isAttaking", true);
                player.GetComponent<Player>().OnHit(damage);
                Debug.Log("" + player.GetComponent<Player>().life);
                recovery = true;
            }
            else
            {
                speed = initialspeed;
                recovery = false;
            }
        }
        else
        {
            if (distance <= stopDistance && recovery == false)
            {
                speed = 0f;
                anim.SetBool("isAttaking", true);
                player.GetComponent<Player>().OnHit(damage);
                Debug.Log("" + player.GetComponent<Player>().life);
                recovery = true;
            }
            else
            {
                speed = initialspeed;
                recovery = false;
            }
        }

        if (recovery)
        {
            recoveryCounter += Time.deltaTime;
            if (recoveryCounter >= recoveryTime)
            {
                recoveryCounter = 0;
                recovery = false;
            }
        }
        else {
            recoveryCounter = 0;
        }


    }

    void FixedUpdate()
    {
        
        if (isDead == false)
        {
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
        if (isDead == false) {
            anim.SetTrigger("hit");
            health--;

            if (health <= 0) {
                SceneController.enemiesDefeated += 1;
                isDead = true;
                anim.SetTrigger("dead");
                Destroy(gameObject, 1f);
            }
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
    }
}
