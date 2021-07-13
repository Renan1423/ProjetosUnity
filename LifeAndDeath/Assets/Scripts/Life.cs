using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    public GameObject Player;
    private SpriteRenderer sprite;
    public Sprite heartSprite;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (Player.GetComponent<Player>().isDead == false)
        {
            sprite.GetComponent<SpriteRenderer>().sprite = null;
        }
        else {
            sprite.GetComponent<SpriteRenderer>().sprite = heartSprite;
        }
    }
}
