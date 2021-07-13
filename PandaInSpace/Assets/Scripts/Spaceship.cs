using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{

    public float speed;
    private float distance;
    public float maxDistance;
    public float minDistance;

    private bool Up;
    private float initialSpeed;

    private Rigidbody2D rig;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        distance = maxDistance / 2;
        Up = true;
        initialSpeed = speed;
    }


    void Update()
    {
        if (Up)
        {
            distance += speed;
            rig.velocity = new Vector2(rig.velocity.x, speed);
            if (distance >= maxDistance)
            {
                StartCoroutine(ChangeDirectionUp());
            }
        }
        else {
            distance -= speed;
            rig.velocity = new Vector2(rig.velocity.x, -speed);
            if (distance <= minDistance)
            {
                StartCoroutine(ChangeDirectionDown());
            }
        }
    }

    IEnumerator ChangeDirectionUp() {

        speed = 0;
        
        yield return new WaitForSeconds(1f);

        if (Up)
        {
            Up = false;

        }
        speed = initialSpeed;
    }

    IEnumerator ChangeDirectionDown()
    {

        speed = 0;

        yield return new WaitForSeconds(1f);

        if(!Up)
        {
            Up = true;
        }
        speed = initialSpeed;
    }
}
