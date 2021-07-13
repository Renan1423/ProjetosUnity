using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody Rig;
    public float speed;

    public float jumpforce;

    private bool estaNoChao;

    // Start is called before the first frame update
    void Start()
    {
        Rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow)) {
            //Rig.velocity = Vector3.left * speed * Time.deltaTime;
            Rig.AddForce(Vector3.left * speed * Time.deltaTime, ForceMode.Acceleration);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            //Rig.velocity = Vector3.right * speed * Time.deltaTime;
            Rig.AddForce(Vector3.right * speed * Time.deltaTime, ForceMode.Acceleration);
        }

        if (Input.GetKeyDown(KeyCode.Space) && estaNoChao) {
            Rig.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
            estaNoChao = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            estaNoChao = true;
        }
    }
}
