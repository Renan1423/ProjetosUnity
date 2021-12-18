using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rig;

    [Header("Status")]

    [SerializeField] private float life = 1;
    [SerializeField] private float maxLife;
    [SerializeField] private float duration = 3f; //time to destroy the gameObject;

    [Header("Movement")]

    [SerializeField] private float hSpeed; // Horizontal Speed
    [SerializeField] private float vSpeed; // Vertical Speed
    [SerializeField] private float hTurnDistance; // distance to change the movement direction
    [SerializeField] private float vTurnDistance; // distance to change the movement direction
    private float distanceX;
    private float distanceY;


    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        rig.velocity = new Vector2(hSpeed, vSpeed);

        life = maxLife;

        if (duration > 0)
        {
            Destroy(gameObject, duration);
        }
    }

    void FixedUpdate()
    {
        // Physics 
        OnMove();
    }

    void OnMove()
    {
        distanceX += Time.deltaTime;
        distanceY += Time.deltaTime;

        if (hTurnDistance > 0 && distanceX >= hTurnDistance) {
            hSpeed = -hSpeed;
            rig.velocity = new Vector2(hSpeed, rig.velocity.y);
            distanceX = 0;
        }

        if (vTurnDistance > 0 && distanceY >= vTurnDistance)
        {
            vSpeed = -vSpeed;
            rig.velocity = new Vector2(rig.velocity.x, vSpeed);
            distanceY = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        // Player's Bullet
        if (collision.tag == "PlayerBullet") {
            life--;

            Destroy(collision.gameObject);

            if (life <= 0)
            {
                // Enemy is dead!
                Destroy(gameObject);
            }
        }

        // Player's Physical Attack
        if (collision.tag == "PlayerAtkPoint") {
            life--;

            collision.gameObject.SetActive(false);

            if (life <= 0)
            {
                // Enemy is dead!
                Destroy(gameObject);
            }
        }
    }
}
