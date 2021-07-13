using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public GameObject player;
    private BoxCollider2D boxcollider;
    void Start()
    {
        boxcollider = GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        if (player.GetComponent<Player>().isDead == true)
        {
            boxcollider.isTrigger = true;
        }
        else {
            boxcollider.isTrigger = false;
        }
    }
}
