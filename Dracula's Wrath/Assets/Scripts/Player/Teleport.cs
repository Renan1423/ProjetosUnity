using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public bool isTouchingGround;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            isTouchingGround = true;
        }
        
        if(collision.gameObject.layer != 9 /*|| collision.gameObject.layer == 0*/)
        {
            isTouchingGround = false;
        }
    }
}
