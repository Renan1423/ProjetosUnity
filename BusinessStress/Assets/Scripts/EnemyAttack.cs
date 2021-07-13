using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private SpriteRenderer spr;

    public float Speed;
    public float Life;

    public bool isRight;
    public bool isLongShot;

    public Sprite[] sprites;

    public GameObject AtkCollider;

    void Start()
    {
        spr = GetComponent<SpriteRenderer>();

        Destroy(gameObject, Life);

        spr.sprite = sprites[Random.Range(0, sprites.Length)];

        if (isRight)
        {
            spr.flipX = true;
        }
        else {
            spr.flipX = false;
        }
    }


    void Update()
    {
        if (isLongShot)
        {
            if (spr.flipX == false)
            {
                transform.Translate(Vector2.left * Time.deltaTime * Speed);
            }
            else
            {
                transform.Translate(Vector2.right * Time.deltaTime * Speed);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AudioController.current.PlayMusic(AudioController.current.punchHeavy);
            if (isLongShot)
            {
                Destroy(gameObject);
            }
            else {
                StartCoroutine(DestroyAtkCollider());
            }
        }
    }

    IEnumerator DestroyAtkCollider() {

        yield return new WaitForSeconds(0.1f);

        Destroy(AtkCollider);
    }
}
