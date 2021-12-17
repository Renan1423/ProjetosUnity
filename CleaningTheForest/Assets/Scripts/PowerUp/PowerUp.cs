using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private Rigidbody2D rig;
    private Transform trans;

    public float Speed;

    public int position;
    private GameObject MovePointPosition;

    public PowerUps powerUpEffect;


    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        trans = GetComponent<Transform>();

        position = Random.Range(1, 6);

        MovePointPosition = GameObject.Find("MovePoint" + position);

        trans.position = new Vector2(MovePointPosition.transform.position.x, trans.position.y - 0.5f);

        Destroy(gameObject, 10f);
    }

    void Update()
    {
        rig.velocity = new Vector2(rig.velocity.x, Speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.tag == "AtkPoint") {

            collision.GetComponent<Player>().playerPowerUp = powerUpEffect;
            collision.GetComponent<Player>().powerUpDurationCount = 0;

            Destroy(gameObject);
        }
    }
}
