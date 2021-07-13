using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int Vida;
    public int Velocidade;
    public int ForcaPulo;

    private bool isJumping;
    private Rigidbody2D rig;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        rig.velocity = new Vector2(Velocidade, rig.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isJumping == false) {
            rig.velocity = new Vector2(rig.velocity.y, ForcaPulo);
            isJumping = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Vida") {
            Vida++;
            Destroy(collision.gameObject);
        }    
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Espinhos") {
            Vida--;
        }

        if (collision.gameObject.layer == 8)
        {
            isJumping = false;
        }
    }


}
