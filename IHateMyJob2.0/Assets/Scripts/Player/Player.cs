using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 5;
    [SerializeField] private float jumpForce = 100;

    private float currentSpeed;
    private Rigidbody rig;
    private Animator anim;
    private Transform groundCheck;
    private bool onGround;
    private bool isDead = false;
    private bool facingLeft = true;
    private bool jump = false;

    void Start()
    {
        rig = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        groundCheck = gameObject.transform.Find("GroundCheck");
        currentSpeed = maxSpeed;
    }

    void Update()
    {
        onGround = Physics.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (Input.GetButtonDown("Jump") && onGround)
        {
            jump = true;
            Debug.Log("JUMPING!!!");
        }
    }

    void FixedUpdate()
    {
        if (!isDead) {
            
            OnMove();

            OnJump();

        }
    }

    void OnMove() {

         //Moving the player

         float h = Input.GetAxis("Horizontal");
         float z = Input.GetAxis("Vertical");

         if (!onGround)
         {
            z = 0;
         }

        rig.velocity = new Vector3(h * currentSpeed, rig.velocity.y, z * currentSpeed);

        //Fliping the sprite

        if (h > 0 && facingLeft)
        {
            Flip();
        }
        else if (h < 0 && !facingLeft) {
            Flip();
        }

    }

    void Flip() {
        facingLeft = !facingLeft;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void OnJump() {
        if (jump) {
            jump = false;
            rig.AddForce(Vector3.up * jumpForce);
        }
    }
}
