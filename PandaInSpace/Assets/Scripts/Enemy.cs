using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed;
    public float life;
    public float minShotTime;
    public float maxShotTime;

    private float spawnTime;
    private float spawnTimeCount;

    public GameObject enemyBullet;
    public Transform shotPoint;

    private Rigidbody2D rig;

    private float initialSpeed;

    private Player PlayerCharacter;

    public float lifeTime;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        spawnTime = Random.Range(minShotTime, maxShotTime);
        initialSpeed = speed;
        speed = Random.Range(1f,initialSpeed);

        PlayerCharacter = GameObject.FindGameObjectWithTag("Playerr").GetComponent<Player>();
    }

    void Update()
    {
        Destroy(gameObject, lifeTime);

        rig.velocity = new Vector2(-speed, rig.velocity.y);

        if (life <= 0) {
            PlayerCharacter.points += 10;
            Destroy(gameObject);
        }

        spawnTimeCount += Time.deltaTime;

        if (spawnTimeCount >= spawnTime) {
            Instantiate(enemyBullet, shotPoint.position, Quaternion.identity);
            spawnTimeCount = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            life--;
        }
    }
}
