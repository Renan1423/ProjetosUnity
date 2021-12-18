using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Cinemachine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]

    public float speed; //Horizontal speed

    public float acceleration;
    
    public float maxSpeed; //Speed Limit

    private float initialSpeed; //Changes the standard speed limit

    public float turnDownSpeed;

    [Header("Jump")]
    [SerializeField]

    public float jumpVelocity;

    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    public float waterMass;

    [Header("Booleans")]
    [SerializeField]

    public bool isDead = true; //Check if the player is dead or alive
    private bool isJumping;
    private bool isFloating;
    private bool isGrounded; //if the player is colliding with the floor
    private bool isMoving; //if the player is moving
    private bool isTurningDown;
    private bool isInWater;

    private Transform trans;
    private Rigidbody2D rig;
    private Animator anim;
    private SpriteRenderer sprite;
    //private BoxCollider2D box;
    private CapsuleCollider2D box;

    [Header("Collision Checks")]
    [SerializeField]

    public LayerMask whatIsWall;
    public LayerMask whatIsGround;
    public float checkRadius;
    public float checkUpRadius;
    public float checkDownRadius;

    private bool isTouchingFront;
    private bool isTouchingBack;
    private bool isTouchingUp;
    private bool isTouchingDown;
    public Transform frontCheck;
    public Transform backCheck;
    public Transform upCheck;
    public Transform downCheck;

    [Header("Wall Jump")]
    [SerializeField]
  
    private bool wallSliding;
    public float wallSlidingSpped;

    bool wallJumping;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;

    [Header("Camera")]
    [SerializeField]

    public GameObject vCam;

    [Header("Damage")]
    [SerializeField]

    private bool isTakingDamage;
    private bool isFreezing;
    private bool canMove = true;

    public float DieTime;

    [Header("UI")]
    [SerializeField]

    public int points;

    public GameObject PausePanel;
    public GameObject EndPanel;
    private bool isPaused;

    public GameObject num;
    public GameObject numFifth;

    public bool StageEnding;

    public GameObject LevelLoader;
    public GameObject SceneEffects;

    public GameObject Particles;
    public GameObject ParticlesFloat;


    #region Start
    void Start()
    {
        
        trans = GetComponent<Transform>();
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        box = GetComponent<CapsuleCollider2D>();
        initialSpeed = maxSpeed;
        StageEnding = false;
    }
    #endregion

    #region Updates
    void Update()
    {
        if (StageEnding == false)
        {
            PauseGame();
        }

        CollisionWithWater();


        if (StageEnding == false)
        {
            float direcao = Input.GetAxis("Horizontal");

            //Move

            OnMove();

            //Jump

            PhysicsJump();
            OnJump();

            //Die

            OnDead();

            //Squat

            OnSquat();

            //Wall slide and wall jump

            OnWallJump();

            //Landing

            OnLand();

            //Floating

            OnFloat();

            //Taking Damage

            if (isTakingDamage)
            {
                OnDamage();
            }
        }
        else
        {
            anim.SetInteger("transition", 0);
            rig.velocity = new Vector2(0, rig.velocity.y);
        }

    }

    

    void FixedUpdate()
    {
        //particles

        particleCheck();

    }

    #endregion

    #region Player Movement

    void OnMove()
    {
        float direcao = Input.GetAxis("Horizontal");

        if(Input.GetKey(KeyCode.RightArrow) && !wallJumping && canMove)
        {

            rig.velocity = new Vector2(speed, rig.velocity.y);

            if (speed >= maxSpeed)
            {
                speed = maxSpeed;
            }
            else {
                speed += acceleration;
            }
            

            isMoving = true;
            if (isGrounded && !isJumping && !isTurningDown && !isTakingDamage)
            {
                anim.SetInteger("transition", 1);
            }
            sprite.flipX = false;
        }

        else if (Input.GetKey(KeyCode.LeftArrow) && !wallJumping && canMove)
        {
            rig.velocity = new Vector2(-speed, rig.velocity.y);

            if (speed <= -maxSpeed)
            {
                speed = -maxSpeed;
            }
            else
            {
                speed -= acceleration;
            }

            isMoving = true;
            if (isGrounded && !isJumping && !isTurningDown)
            {
                anim.SetInteger("transition", 1);
            }
            sprite.flipX = true;
        }

        else
        {
            speed = 0;
            rig.velocity = new Vector2(speed, rig.velocity.y);
            isMoving = false;
            if (isGrounded)
            {
                anim.SetInteger("transition", 0);
            }
            else {
                anim.SetInteger("transition", 3);
            }
        }

        /*

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
        */

    }
    #endregion

    #region Player jump

    void PhysicsJump()
    {
        if (rig.velocity.y < 0)
        {
            rig.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rig.velocity.y > 0 && !Input.GetKey(KeyCode.UpArrow))
        {
            rig.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

    }

    void OnJump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && !isTurningDown && canMove && !isTakingDamage && !isJumping && isTouchingDown) {
            AudioController.current.PlayMusic(AudioController.current.jump);
            rig.velocity = Vector2.up * jumpVelocity;
            isGrounded = false;
            isJumping = true;
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


        /*
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
        }*/

    }
    void OnFloat()
    {
        if (isJumping && isDead)
        {
            if (Input.GetKey(KeyCode.UpArrow) && rig.velocity.y < 0 && isDead)
            {
                isFloating = true;
            }
            else
            {
                isFloating = false;
                fallMultiplier = 2.5f;
            }
        }

        if (isFloating) {
            fallMultiplier = 0.1f;
            if (!isDead)
            {
                isFloating = false;
                fallMultiplier = 2.5f;
            }
        }

        if (!isDead) {
            isFloating = false;
            fallMultiplier = 2.5f;
        }
    }

    void OnWallJump() 
    {

        float direcao = Input.GetAxis("Horizontal");


        //Check Collision

        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsWall);
        isTouchingBack = Physics2D.OverlapCircle(backCheck.position, checkRadius, whatIsWall);

        if (isTouchingFront == true && isGrounded == false && direcao != 0 && isDead == false && isJumping)
        {
            wallSliding = true;
        }
        else
        {
            wallSliding = false;
        }

        if (isTouchingBack == true && isGrounded == false && direcao != 0 && isDead == false && isJumping)
        {
            wallSliding = true;
        }
        else
        {
            //wallSliding = false;
        }

        //Wall Sliding

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

        //Wall Jump

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
    }

    void SetWallJumpingToFalse()
    {
        wallJumping = false;
    }

    void OnLand() 
    {
        isTouchingDown = Physics2D.OverlapCircle(downCheck.position, checkDownRadius, whatIsGround);

        if (isTouchingDown)
        {
            isGrounded = true;
        }
        else {
            isGrounded = false;
        }

        if (!isGrounded && !isJumping)
        {
            anim.SetInteger("transition", 3);
        }

        if (isGrounded && !isTurningDown)
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
    }
    #endregion

    #region Squat

    void OnSquat() {

        //Setting speed to normal

        if (!isTurningDown)
        {
            speed = initialSpeed;
        }

        if (!isDead && isGrounded)
        {
            //Check collision

            isTouchingUp = Physics2D.OverlapCircle(upCheck.position, checkUpRadius, whatIsWall);
            if (Input.GetKey(KeyCode.DownArrow))
            {
                if (isJumping) {
                    isJumping = false;
                }
                if (!wallJumping && !isJumping)
                {
                    isTurningDown = true;
                    speed = turnDownSpeed;
                    anim.SetInteger("transition", 6);
                    box.GetComponent<CapsuleCollider2D>().size = new Vector2(0.6f, 0.6f);
                    box.GetComponent<CapsuleCollider2D>().offset = new Vector2(box.GetComponent<CapsuleCollider2D>().offset.x, -0.5f);
                }
            }
            else
            {
                if (!isTouchingUp)
                {
                    isTurningDown = false;
                    box.GetComponent<CapsuleCollider2D>().size = new Vector2(1.13f, 1.585177f);
                    box.GetComponent<CapsuleCollider2D>().offset = new Vector2(box.GetComponent<CapsuleCollider2D>().offset.x, -0.030343f);
                }
                else
                {
                    isTurningDown = true;
                    speed = turnDownSpeed;
                    anim.SetInteger("transition", 6);
                    box.GetComponent<CapsuleCollider2D>().size = new Vector2(0.6f, 0.6f);
                    box.GetComponent<CapsuleCollider2D>().offset = new Vector2(box.GetComponent<CapsuleCollider2D>().offset.x, -0.5f);
                }

            }

            if (!Input.GetKey(KeyCode.DownArrow) && !isTouchingUp)
            {
                isTurningDown = false;
                box.GetComponent<BoxCollider2D>().size = new Vector2(box.GetComponent<BoxCollider2D>().size.x, 1.585177f);
                box.GetComponent<BoxCollider2D>().offset = new Vector2(box.GetComponent<BoxCollider2D>().offset.x, -0.030343f);
            }
        }
    }
    #endregion

    #region Collisions

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
        else {
            isGrounded = false;
        }

        if(collision.gameObject.layer == 12)
        {
            rig.AddForce(new Vector2(0, 12), ForceMode2D.Impulse);
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        float direcao = Input.GetAxis("Horizontal");

        //Collecting KEY and ending stage

        if (collision.gameObject.tag == "Key")
        {
            if (collision.GetComponent<Key>().isCollectable == true)
            {
                SceneEffects.SetActive(false);
                AudioController.current.PlayMusic(AudioController.current.getItem);

                //Ending Stage

                EndPanel.GetComponent<PauseController>().EndStage();
                StageEnding = true;
                Destroy(collision.gameObject);
            }

        }

        if (collision.gameObject.layer == 11) { //DEAD LINE - the player dies if falls from the stage
            anim.SetInteger("transition", 5);
            rig.gravityScale = 0.1f;
            LevelLoader.GetComponent<LevelLoader>().LoadThisLevel();
        }

        //Collecting Points

        if (collision.gameObject.tag == "Ponto")
        {
            GameObject number = Instantiate(numFifth);
            number.transform.position = trans.transform.position;
            AudioController.current.PlayMusic(AudioController.current.getItem);
            Destroy(collision.gameObject);
            points += 50;
        }

        //Itens if is Dead

        if (isDead)
        {

            //Revive item - Heart

            if (collision.gameObject.tag == "Item")
            {
                AudioController.current.PlayMusic(AudioController.current.getItem);
                Destroy(collision.gameObject);
                SceneEffects.GetComponent<SceneEffects>().FadeReviveLevel();
                isDead = false;
                isFloating = false;
                anim.SetInteger("transition", 5);
            }

            //Water

            if (collision.gameObject.tag == "Water")
            {
                isInWater = true;
                anim.SetInteger("transition", 5);
                rig.gravityScale = waterMass;
                LevelLoader.GetComponent<LevelLoader>().LoadThisLevel();
            }
            else {
                isInWater = false;
                rig.gravityScale = 1f;
            }

        }

        //Itens if is Alive

        if (!isDead)
        {

            //Taking Damage from spikes

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
                rig.gravityScale = 0.5f;
            }
            else {
                isInWater = false;
                rig.gravityScale = waterMass;
            }

            //Taking Damage from enemy

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

    void CollisionWithWater()
    {
        if (!isDead)
        {
            if (isInWater)
            {
                rig.gravityScale = waterMass;
            }
            else
            {
                rig.gravityScale = 1f;
            }
        }

        if (isDead)
        {
            if (isInWater)
            {
                fallMultiplier = 0.1f;
                rig.gravityScale = 0.1f;
                LevelLoader.GetComponent<LevelLoader>().LoadThisLevel();
            }
            else
            {
                rig.gravityScale = 1f;
            }
        }
    }

    #endregion

    #region Taking Damage

    void TakeDamage()
    {
        anim.SetInteger("transition", 5);
        if (vCam.activeSelf == false) {
            vCam.SetActive(true);
        }
        vCam.GetComponent<CameraController>().CameraShake();
        points -= 100;
        //speed = initialSpeed;
        isTurningDown = false;
        isMoving = false;
    }

    void OnDamage() 
    {
        float direcao = Input.GetAxis("Horizontal");
        anim.SetInteger("transition", 5);
        canMove = false;
        if (isGrounded)
        {
            //rig.AddForce(new Vector2(10 * -direcao, 10), ForceMode2D.Force);
            rig.AddForce(new Vector2(0, 12), ForceMode2D.Force);
        }
        else
        {
            //rig.AddForce(new Vector2(5 * -direcao, 10), ForceMode2D.Impulse);
            rig.AddForce(new Vector2(0, 15), ForceMode2D.Force);
        }
        //isTakingDamage = false;
        StartCoroutine("Freeze");
    }


    //Cooldown after taking damage

    IEnumerator Freeze() {
        isFreezing = true;
        canMove = false;

        yield return new WaitForSeconds(.4f);

        isDead = true;
        
        canMove = true;
        isTakingDamage = false;
        isFreezing = false;
        anim.SetInteger("transition", 0);
    }

    //Change player's weight

    void OnDead()
    {
        if (isDead)
        {
            anim.SetBool("isDead", true);
            rig.mass = 0.85f;
            jumpVelocity = 9f;
        }
        else
        {
            anim.SetBool("isDead", false);
            rig.mass = 1f;
            jumpVelocity = 7.2f;
        }
    }
    #endregion

    #region Pause

    void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!isPaused)
            {
                isPaused = true;
                PausePanel.GetComponent<PauseController>().Pause();
            }
            else {
                isPaused = false;
                PausePanel.GetComponent<PauseController>().Resume();
            }
            
        }
    }

    #endregion Pause

    #region Particle System

    void particleCheck()
    {
        if (isDead)
        {
            Particles.SetActive(true);
        }
        else {
            Particles.SetActive(false);
        }

        if (Input.GetKey(KeyCode.UpArrow) && rig.velocity.y < 0 && !isTakingDamage && isDead)
        {
            ParticlesFloat.SetActive(true);
        }
        else
        {
            ParticlesFloat.SetActive(false);
        }
    }
    #endregion

    #region Draw Gizmos

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(frontCheck.position, checkRadius);
        Gizmos.DrawWireSphere(backCheck.position, checkRadius);
        Gizmos.DrawWireSphere(upCheck.position, checkUpRadius);
        Gizmos.DrawWireSphere(downCheck.position, checkDownRadius);
        Gizmos.DrawWireSphere(frontCheck.position, checkRadius);
        Gizmos.DrawWireSphere(backCheck.position, checkRadius);
    }

    #endregion

}
