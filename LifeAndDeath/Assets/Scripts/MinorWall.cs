using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinorWall : MonoBehaviour
{
    public GameObject player;
    //private SpriteRenderer sprite;

    private Collider2D Compcollider;

    void Start()
    {
        //sprite = GetComponent<SpriteRenderer>();

        Compcollider = GetComponent<Collider2D>();

    }


    void Update()
    {
        //Tile opacity

        if (player.GetComponent<Player>().isDead == true)
        {
            Compcollider.isTrigger = true;
        }
        else {
            Compcollider.isTrigger = false;
        }


        //Object opacity
        /*
        if (player.GetComponent<Player>().isDead == true)
        {
            sprite.color = new Color(1, 1, 1, 0.6f);
        }
        else
        {
            sprite.color = new Color(1, 1, 1, 1f);
        }*/
    }
}
