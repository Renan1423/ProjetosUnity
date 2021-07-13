using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public Sprite[] powerUpSprites;

    public float life;
    public float speed;
    private int numPowerUps;
    private int choosePowerUp;
    private PowerUps powerUp;

    private Player Player;

    private Rigidbody2D rig;
    private SpriteRenderer spr;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        spr = GetComponent<SpriteRenderer>();

        numPowerUps = powerUpSprites.Length;
        choosePowerUp = Random.Range(0, numPowerUps);

        spr.sprite = powerUpSprites[choosePowerUp];

        if (choosePowerUp == 0) {
            powerUp = PowerUps.BIG;
        }else if (choosePowerUp == 1)
        {
            powerUp = PowerUps.DOUBLE;
        }else if (choosePowerUp == 2)
        {
            powerUp = PowerUps.FAST;
        }

        Player = GameObject.FindGameObjectWithTag("Playerr").GetComponent<Player>();

        speed = Random.Range(6f,18f);


    }

    void Update()
    {
        rig.velocity = new Vector2(-speed, rig.velocity.y);
        Destroy(gameObject, life);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Playerr")
        {
            Destroy(gameObject);
            Player.powerUp = powerUp;
        }
    }

}