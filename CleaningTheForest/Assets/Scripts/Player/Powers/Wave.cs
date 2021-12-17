using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    private Rigidbody2D rig;

    public float speed;
    public int life = 2;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();

        transform.position -= new Vector3(0, 0.3f, 0);

        Destroy(gameObject, 5f);
    }


    void Update()
    {
        rig.velocity = new Vector2(0f, -speed);
    }

}
