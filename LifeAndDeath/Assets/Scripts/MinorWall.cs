using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinorWall : MonoBehaviour
{
    public GameObject player;
    private SpriteRenderer sprite;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (player.GetComponent<Player>().isDead == true)
        {
            sprite.color = new Color(1, 1, 1, 0.6f);
        }
        else
        {
            sprite.color = new Color(1, 1, 1, 1f);
        }
    }
}
