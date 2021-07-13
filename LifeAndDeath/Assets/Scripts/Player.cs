using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cinemachine;

public class Player : MonoBehaviour
{
    public float speed;
    public float turnDownSpeed;
    private float initialSpeed;
    public bool isDead = true;
    public float jumpForce;
    public int points;
    public float waterMass;
    private bool isInWater;

    private bool isJumping;
    private bool isGrounded;
    private bool isMoving;
    private bool isTurningDown;

    private Transform trans;
    private Rigidbody2D rig;
    private Animator anim;
    private SpriteRenderer sprite;
    private BoxCollider2D box;

    public LayerMask whatIsGround;
    public float checkRadius;
    public float checkUpRadius;
    private bool isTouchingFront;
    private bool isTouchingBack;
    private bool isTouchingUp;
    public Transform frontCheck;
    public Transform backCheck;
    public Transform upCheck;
    private bool wallSliding;
    public float wallSlidingSpped;

    bool wallJumping;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;

    public GameObject vCam;
    private bool isTakingDamage;
    private bool isFreezing;
    private bool canMove = true;

    public float DieTime;

    public GameObject PausePanel;
    public GameObject EndPanel;

    public GameObject num;
    public GameObject numFifth;

    public bool StageEnding;

    public GameObject LevelLoader;
    public GameObject SceneEffects;

    public GameObject Particles;
    public GameObject ParticlesFloat;

    void Start()
    {
        trans = GetComponent<Transform>();
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();
        initialSpeed = speed;
        StageEnding = false;
    }

    void Update()
    {
        if (StageEnding == false)
        {
            PauseGame();
        }
        if (!isDead) {
            if (isInWater)
            {
                rig.gravityScale = waterMass;
            }
            else
            {
                rig.gravityScale = 1f;
            }
        }

        if (isDead) {
            if (isInWater)
            {
                rig.gravityScale = 0.1f;
            }
            else {
                rig.gravityScale = 1f;
            }
        }
    }

    void FixedUpdate()
    {
        particleCheck();

        if (StageEnding == false)
        {
            float direcao = Input.GetAxis("Horizontal");

            OnMove();
            OnJump();
            OnDead();
            if (!isGrounded && !isJumping)
            {
                anim.SetInteger("transition", 3);
            }

            if (isGrounded)
            {
                if (!isMoving)
                {
                    anim.SetInteger("transition", 0);
                }
                else
                {
                    anim.SetInteger("transition", 1);
                }
            }

            if (!isTurningDown)
            {
                speed = initialSpeed;
            }

            isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsGround);
            isTouchingBack = Physics2D.OverlapCircle(backCheck.position, checkRadius, whatIsGround);
            if (isTouchingFront == true && isGrounded == false && direcao != 0 && isDead == false)
            {
                wallSliding = true;
            }
            else
            {
                wallSliding = false;
            }

            if (isTouchingBack == true && isGrounded == false && direcao != 0 && isDead == false)
            {
                wallSliding = true;
            }
            else
            {
                //wallSliding = false;
            }

            if (wallSliding)
            {
                rig.velocity = new Vector2(rig.velocity.x, Mathf.Clamp(rig.velocity.y, -wallSlidingSpped, float.MaxValue));
                if (sprite.flipX == false)
                {
                    sprite.flipX = true;
                }
                else
                {
                    sprite.flipX = false;
                }
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) && wallSliding == true)
            {
                AudioController.current.PlayMusic(AudioController.current.jump);
                wallJumping = true;
                Invoke("SetWallJumpingToFalse", wallJumpTime);
            }
            if (wallJumping)
            {
                rig.velocity = new Vector2(xWallForce * -direcao, yWallForce);
           }


            if (!isDead)
            {
                isTouchingUp = Physics2D.OverlapCircle(upCheck.position, checkUpRadius, whatIsGround);
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    if (!wallJumping && !isJumping)
                    {
                        isTurningDown = true;
                        speed = turnDownSpeed;
                        anim.SetInteger("transition", 6);
                        box.GetComponent<BoxCollider2D>().size = new Vector2(box.GetComponent<BoxCollider2D>().size.x, 1f);
                        box.GetComponent<BoxCollider2D>().offset = new Vector2(box.GetComponent<BoxCollider2D>().offset.x, -0.27f);
                    }
                }
                else
                {
                    if (!isTouchingUp)
                    {
                        isTurningDown = false;
                        box.GetComponent<BoxCollider2D>().size = new Vector2(box.GetComponent<BoxCollider2D>().size.x, 1.585177f);
                        box.GetComponent<BoxCollider2D>().offset = new Vector2(box.GetComponent<BoxCollider2D>().offset.x, -0.030343f);
                    }
                    else {
                        isTurningDown = true;
                        speed = turnDownSpeed;
                        anim.SetInteger("transition", 6);
                        box.GetComponent<BoxCollider2D>().size = new Vector2(box.GetComponent<BoxCollider2D>().size.x, 1f);
                        box.GetComponent<BoxCollider2D>().offset = new Vector2(box.GetComponent<BoxCollider2D>().offset.x, -0.27f);
                    }

                }

                if (!Input.GetKey(KeyCode.DownArrow) && !isTouchingUp) {
                    isTurningDown = false;
                    box.GetComponent<BoxCollider2D>().size = new Vector2(box.GetComponent<BoxCollider2D>().size.x, 1.585177f);
                    box.GetComponent<BoxCollider2D>().offset = new Vector2(box.GetComponent<BoxCollider2D>().offset.x, -0.030343f);
                }
            }

            if (isDead)
            {
                if (isJumping)
                {
                    if (Input.GetKey(KeyCode.UpArrow) && rig.velocity.y < 0 && !isTakingDamage)
                    {
                        rig.gravityScale = 0.1f;
                    }
                    else
                    {
                        rig.gravityScale = 1f;
                    }
                }
            }

            if (isTakingDamage)
            {
                canMove = false;
                if (isGrounded)
                {
                    rig.AddForce(new Vector2(200 * -direcao, 200), ForceMode2D.Force);
                }
                else
                {
                    rig.AddForce(new Vector2(5 * -direcao, 10), ForceMode2D.Impulse);
                }
                isTakingDamage = false;
                StartCoroutine("Freeze");
            }
        }
        else {
            anim.SetInteger("transition", 0);
            rig.velocity = new Vector2(0, rig.velocity.y);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isJumping = false;
            isGrounded = true;
            if (!isMoving)
            {
                anim.SetInteger("transition", 0);
            }
            else
            {
                anim.SetInteger("transition", 1);
            }
        }

        if(collision.gameObject.layer == 12)
        {
            rig.AddForce(new Vector2(0, 12), ForceMode2D.Impulse);
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        float direcao = Input.GetAxis("Horizontal");

        if (collision.gameObject.tag == "Key")
        {
            SceneEffects.SetActive(false);
            AudioController.current.PlayMusic(AudioController.current.getItem);
            Destroy(collision.gameObject);
            EndPanel.GetComponent<PauseController>().EndStage();
            StageEnding = true;
            
        }

        if (collision.gameObject.layer == 11) { //DEAD END
            anim.SetInteger("transition", 5);
            rig.gravityScale = 0.1f;
            LevelLoader.GetComponent<LevelLoader>().LoadThisLevel();
        }

        if (collision.gameObject.tag == "Ponto")
        {
            GameObject number = Instantiate(numFifth);
            number.transform.position = trans.transform.position;
            AudioController.current.PlayMusic(AudioController.current.getItem);
            Destroy(collision.gameObject);
            points += 50;
        }

        if (isDead)
        {
            if (collision.gameObject.tag == "Item")
            {
                AudioController.current.PlayMusic(AudioController.current.getItem);
                Destroy(collision.gameObject);
                SceneEffects.GetComponent<SceneEffects>().FadeReviveLevel();
                isDead = false;
                anim.SetInteger("transition", 5);
            }
            if (collision.gameObject.tag == "Water")
            {
                isInWater = true;
                anim.SetInteger("transition", 5);
                rig.gravityScale = 0.1f;
                LevelLoader.GetComponent<LevelLoader>().LoadThisLevel();
            }
            else {
                isInWater = false;
            }

        }

        if (!isDead)
        {
            if (collision.gameObject.tag == "Spike" && !isFreezing)
            {
                GameObject number = Instantiate(num);
                number.transform.position = trans.transform.position;
                AudioController.current.PlayMusic(AudioController.current.hitHurt);
                SceneEffects.GetComponent<SceneEffects>().FadeDieLevel();
                isTakingDamage = true;
                TakeDamage();
            }
            if (collision.gameObject.tag == "Water")
            {
                isInWater = true;
            }
            else {
                isInWater = false;
            }

            if (collision.gameObject.tag == "Enemy" && !isFreezing)
            {
                GameObject number = Instantiate(num);
                number.transform.position = trans.transform.position;
                AudioController.current.PlayMusic(AudioController.current.hitHurt);
                SceneEffects.GetComponent<SceneEffects>().FadeDieLevel();
                isTakingDamage = true;
                TakeDamage();
            }
         }
    }

    void TakeDamage()
    {
        isDead = true;
        anim.SetInteger("transition", 5);
        vCam.GetComponent<CameraController>().CameraShake();
        points -= 100;
        speed = initialSpeed;
        isTurningDown = false;
    }

    IEnumerator Freeze() {
        isFreezing = true;
        canMove = false;

        yield return new WaitForSeconds(.4f);

        canMove = true;
        isTakingDamage = false;
        isFreezing = false;
    }

    void OnMove()
    {
        float direcao = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.RightArrow) && !wallJumping && canMove)
        {
            rig.velocity = new Vector2(speed, rig.velocity.y);
            isMoving = true;
            if (isGrounded && !isJumping && !isTurningDown)
            {
                anim.SetInteger("transition", 1);
            }
            sprite.flipX = false;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && !wallJumping && canMove)
        {
            rig.velocity = new Vector2(-speed, rig.velocity.y);
            isMoving = true;
            if (isGrounded && !isJumping && !isTurningDown)
            {
                anim.SetInteger("transition", 1);
            }
            sprite.flipX = true;
        }
        else
        {
            rig.velocity = new Vector2(0, rig.velocity.y);
            isMoving = false;
            anim.SetInteger("transition", 0);
        }

    }

    void OnJump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isTurningDown && canMove && !isTakingDamage)
        {
            if (isJumping == false)
            {
                AudioController.current.PlayMusic(AudioController.current.jump);
                rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isGrounded = false;
                isJumping = true;
            }
        }

        if (isJumping == true && canMove)
        {
            if (rig.velocity.y >= 0)
            {
                anim.SetInteger("transition", 2);
            }
            if (rig.velocity.y < 0)
            {
                anim.SetInteger("transition", 3);
            }
        }

    }

    void SetWallJumpingToFalse() {
        wallJumping = false;
    }

    void OnDead() {
        if (isDead)
        {
            anim.SetBool("isDead", true);
            rig.mass = 0.85f;
        }
        else {
            anim.SetBool("isDead", false);
            rig.mass = 1f;
        }
    }

    void PauseGame()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            PausePanel.GetComponent<PauseController>().Pause();
        }
    }

    void particleCheck()
    {
        if (isDead)
        {
            Particles.SetActive(true);
        }
        else {
            Particles.SetActive(false);
        }

        if (Input.GetKey(KeyCode.UpArrow) && rig.velocity.y < 0 && !isTakingDamage)
        {
            ParticlesFloat.SetActive(true);
        }
        else
        {
            ParticlesFloat.SetActive(false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(frontCheck.position, checkRadius);
        Gizmos.DrawWireSphere(backCheck.position, checkRadius);
        Gizmos.DrawWireSphere(upCheck.position, checkUpRadius);
    }

}
