using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonicWave : MonoBehaviour
{
    public float Speed;
    public float atkRadius;
    public LayerMask enemyLayer;
    private Player player;
    private SpriteRenderer spritePlayer;
    private SpriteRenderer sprite;

    private bool direction;

    void Start()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, atkRadius, enemyLayer);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        spritePlayer = player.GetComponent<SpriteRenderer>();
        direction = spritePlayer.flipX;
        sprite = GetComponent<SpriteRenderer>();

        Destroy(gameObject, 5f);
    }

    void Update()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, atkRadius, enemyLayer);
        if (hit != null)
        {
            hit.GetComponent<Enemy>().OnHit();
        }
        if (!direction)
        {
            sprite.flipX = false;
            transform.Translate(Vector2.right * Time.deltaTime * Speed);
        }
        else {
            sprite.flipX = true;
            transform.Translate(Vector2.right * Time.deltaTime * -Speed);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, atkRadius);
    }
}
