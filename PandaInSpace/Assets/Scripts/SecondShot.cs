using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondShot : MonoBehaviour
{
    public float speed;
    public float verticalSpeedMin;
    public float verticalSpeedMax;
    public float life;

    private Rigidbody2D rig;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        rig.velocity = new Vector2(speed, Random.Range(verticalSpeedMin, verticalSpeedMax));
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }

}
