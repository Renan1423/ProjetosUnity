using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{

    private Rigidbody2D rig;
    private float distance;
    private bool max;
    public float maxDistance;
    public float cloudSpeed;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        if (distance <= maxDistance && max == false)
        {
            rig.velocity = new Vector2(cloudSpeed, rig.velocity.y);
            distance += cloudSpeed;
            if (distance >= maxDistance) {
                max = true;
            }
        } else if (max == true) 
          {
            rig.velocity = new Vector2(-cloudSpeed, rig.velocity.y);
            distance -= cloudSpeed;
            if (distance <= (-maxDistance + 50))
            {
                max = false;
            }
        }
    }
}
