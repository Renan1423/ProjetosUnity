using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindWave : MonoBehaviour
{
    public float Speed;
    private Player player;
    private SpriteRenderer spritePlayer;
    private SpriteRenderer sprite;

    private bool direction;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        spritePlayer = player.GetComponent<SpriteRenderer>();
        direction = spritePlayer.flipX;
        sprite = GetComponent<SpriteRenderer>();

        Destroy(gameObject, 0.7f);
    }

    void Update()
    {
        if (!direction)
        {
            sprite.flipX = false;
            transform.Translate(Vector2.right * Time.deltaTime * Speed);
        }
        else
        {
            sprite.flipX = true;
            transform.Translate(Vector2.right * Time.deltaTime * -Speed);
        }
    }
}
