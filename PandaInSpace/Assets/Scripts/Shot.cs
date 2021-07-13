using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    public float speed;
    public float life;
    public GameObject secondBulletPrefab;
    public GameObject secondBulletPrefab2;
    public GameObject secondBulletPrefab3;

    public PowerUps powerUps;

    private Rigidbody2D rig;
    private Transform trans;
    private BoxCollider2D boxCollider;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        trans = GetComponent<Transform>();
        boxCollider = GetComponent<BoxCollider2D>();

        if (powerUps == PowerUps.BIG)
        {
            trans.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            boxCollider.size = new Vector2(1.7f, 0.9f);
        }
        else { 
        
        }
    }

    void Update()
    {
        if (powerUps != PowerUps.DOUBLE)
        {
            rig.velocity = new Vector2(speed, rig.velocity.y);
            Destroy(gameObject, life);
        }
        else {
            Instantiate(secondBulletPrefab3, trans.position, Quaternion.identity);
            Instantiate(secondBulletPrefab2, trans.position, Quaternion.identity);
            Instantiate(secondBulletPrefab, trans.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }

}
