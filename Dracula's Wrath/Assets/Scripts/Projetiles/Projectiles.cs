using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{

    private Rigidbody2D rig;
    private SpriteRenderer spr;
    private Collider2D col;

    public float life;
    public float Damage;

    public bool isFromPlayer;


    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        Destroy(gameObject, life);
    }

    private void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 && collision.tag != "Box") { //collision with walls
            spr.sprite = null;
            col.GetComponent<CircleCollider2D>().radius = 0;
            Destroy(gameObject, 1f);
        }

        if (collision.tag == "Enemy" && isFromPlayer) {
            StartCoroutine(collision.GetComponent<Enemy>().TakeDamage(Damage + transform.localScale.x));
        }
        
        if (collision.tag == "Player" && !isFromPlayer)
        {
            StartCoroutine(collision.GetComponent<Player>().TakeDamage(Damage));
            Destroy(gameObject);
        }

        if (collision.tag == "BloodDrain") {
            Destroy(gameObject);
        }

        if (collision.tag == "Skeleton" && !isFromPlayer)
        {
            StartCoroutine(collision.GetComponent<Skeleton>().TakeDamage(Damage));
            Destroy(gameObject);
        }

        if (collision.gameObject.layer == 8 && collision.tag == "Box")
        { //collision with walls
            spr.sprite = null;
            col.GetComponent<CircleCollider2D>().radius = 0;
            StartCoroutine(collision.GetComponent<BoxScript>().TakeDamage(Damage));
            Destroy(gameObject, 1f);
        }
    }
}
