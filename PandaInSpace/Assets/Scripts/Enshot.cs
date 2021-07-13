using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enshot : MonoBehaviour
{

    public float initialSpeed;
    public float life;
    private float speed;
    private float verticalSpeed;

    private Rigidbody2D rig;

    void Start()
    {
        speed = initialSpeed;
        speed = Random.Range(-3f, initialSpeed);
        rig = GetComponent<Rigidbody2D>();
        verticalSpeed = Random.Range(2f, -2f);
    }

    void Update()
    {
        rig.velocity = new Vector2(speed, verticalSpeed);
        Destroy(gameObject, life);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Playerr")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Spaceship")
        {
            Destroy(gameObject);
        }
    }

}
