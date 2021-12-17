using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tsunami : MonoBehaviour
{

    private Rigidbody2D rig;

    public float speed;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();

        transform.position += new Vector3(0, 1f, 0);

        Destroy(gameObject, 8f);
    }


    void Update()
    {
        rig.velocity = new Vector2(0f, -speed);
    }

}

