using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PowerUps { NORMAL, DOUBLE, BIG, FAST}

public class Player : MonoBehaviour
{

    public float speed;
    public float spaceShipSpeed;
    public float jumpForce;
    private Rigidbody2D rig;
    private Animator anim;

    private bool isJumping;
    private bool isShooting;
    private bool isTakingDamage;

    public GameObject bullet;
    public Transform bulletPoint;
    public Shot shotScript;
    public float bulletDamage;

    public Text Lifes;
    public int numLifes;
    public Text Points;
    public int points;
    public GameObject PausePanel;
    public GameObject GameOverPanel;

    public Rigidbody2D spaceShip;
    public Animator spaceShipAnim;

    public PowerUps powerUp;
    public float limitTimePowerUp = 10;

    public Transform groundCheckPoint;
    public float checkRadius;
    public LayerMask whatIsGround;
    public LayerMask whatIsEnemy;
    private bool isGrounded;
    private bool isJumpingIntoEnemy;

    public float hangTime;
    private float hangCounter;

    private bool isDashing;
    public float dashTime;
    public float dashSpeed;
    public float distanceBetweenImages;
    public float dashCoolDown;
    private float dashTimeLeft;
    private float lastImageXpos;
    private float lastDash = -100;

    public PowerUpImage powImage;

    private bool isDead;

    

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        shotScript.powerUps = PowerUps.NORMAL;

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spaceship")
        {
            isJumping = false;
        }

        if (collision.gameObject.tag == "DeadEnd") {
            numLifes = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyBullet")
        {
            if (numLifes >= 0 && !isTakingDamage)
            {
                numLifes--;
                StartCoroutine(Feedback());
            }
        }

        if (collision.gameObject.layer == 10 && isJumpingIntoEnemy)
        {
            Destroy(collision.gameObject);
        }
    }

    void Update()
    {
        if (!isDead)
        {
            CheckDash();
            CheckPowerUp();

            if (Input.GetKey(KeyCode.RightArrow))
            {
                rig.velocity = new Vector2(speed, rig.velocity.y);
            }

            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                rig.velocity = new Vector2(-speed, rig.velocity.y);
            }
            else
            {
                rig.velocity = new Vector2(0, rig.velocity.y);
            }

            isJumpingIntoEnemy = Physics2D.OverlapCircle(groundCheckPoint.position, checkRadius, whatIsEnemy);

            if (isJumpingIntoEnemy && isDashing)
            {
                isDashing = false;
                rig.AddForce((transform.up * jumpForce * 4), ForceMode2D.Impulse);
            }
            else {
                isJumpingIntoEnemy = false;
            }

            isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, checkRadius, whatIsGround);

            if (isGrounded)
            {
                hangCounter = hangTime;
            }
            else
            {
                hangCounter -= Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded && hangCounter > 0f)
            {
                //if (!isJumping)
                //{
                //    rig.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
                //    isJumping = true;
                //}

                rig.velocity = new Vector2(rig.velocity.x, jumpForce);
                isJumping = true;
            }

            if (Input.GetKeyUp(KeyCode.UpArrow) && rig.velocity.y > 0)
            {
                rig.velocity = new Vector2(rig.velocity.x, rig.velocity.y * .5f);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                //if (isJumping && !isGrounded)
                //{
                //    rig.AddForce(transform.up * (-jumpForce - 1), ForceMode2D.Impulse);
                //}
                if (Time.time >= (lastDash + dashCoolDown))
                {
                    AttemptToDash();
                }
            }

            if (Input.GetKey(KeyCode.S))
            {
                if (!isShooting)
                {
                    StartCoroutine(Shooting());
                }
            }

            if (Input.GetKey(KeyCode.D))
            {
                spaceShip.velocity = new Vector2(spaceShipSpeed, rig.velocity.y);
                spaceShipAnim.SetInteger("Dir", 1);
            }

            else if (Input.GetKey(KeyCode.A))
            {
                spaceShip.velocity = new Vector2(-spaceShipSpeed, rig.velocity.y);
                spaceShipAnim.SetInteger("Dir", 2);
            }
            else
            {
                spaceShip.velocity = new Vector2(0, rig.velocity.y);
                spaceShipAnim.SetInteger("Dir", 0);
            }

        }

        if (numLifes > 0)
        {
            GameOverPanel.SetActive(false);
            Lifes.text = "x " + numLifes;
        }
        else
        {
            Lifes.text = "x 0";
            GameOverPanel.SetActive(true);
            isDead = true;
            //Destroy(gameObject);
        }

        if (points < 100)
        {
            Points.text = "00" + points;
        }
        else if (points < 1000 && points >= 100)
        {
            Points.text = "0" + points;
        }
        else {
            Points.text = "" + points;
        }
        
    }

    private void AttemptToDash() {
        isDashing = true;
        dashTimeLeft = dashTime;
        lastDash = Time.time;

        PlayerAfterImagePool.Instance.GetFromPool();
        lastImageXpos = transform.position.y;
    }

    private void CheckDash() {
        if (isDashing) {
            if (dashTimeLeft > 0) {
                rig.velocity = new Vector2(rig.velocity.x, -dashSpeed);
                dashTimeLeft -= Time.deltaTime;

                if (Mathf.Abs(transform.position.y - lastImageXpos) > distanceBetweenImages)
                {
                    PlayerAfterImagePool.Instance.GetFromPool();
                    lastImageXpos = transform.position.y;
                }
            }

            if (dashTimeLeft <= 0) {
                isDashing = false;
            }
        }
    }

    void CheckPowerUp() {
        if (powerUp == PowerUps.BIG)
        {
            shotScript.powerUps = PowerUps.BIG;
            bulletDamage = 2;
            powImage.SelectPowerUp = 1;
            ChangeToNormalPowerUp();
        }
        else if (powerUp == PowerUps.FAST)
        {
            shotScript.powerUps = PowerUps.FAST;
            bulletDamage = 0.5f;
            powImage.SelectPowerUp = 3;
            ChangeToNormalPowerUp();
        }
        else if (powerUp == PowerUps.DOUBLE)
        {
            shotScript.powerUps = PowerUps.DOUBLE;
            bulletDamage = 1f;
            powImage.SelectPowerUp = 2;
            ChangeToNormalPowerUp();
        }
    }

    void ChangeToNormalPowerUp() {
        if (powerUp != PowerUps.NORMAL)
        {
            StartCoroutine(ChangeToNormal());
        }
    }

    IEnumerator ChangeToNormal() {
        yield return new WaitForSeconds(limitTimePowerUp);

        powerUp = PowerUps.NORMAL;
        shotScript.powerUps = PowerUps.NORMAL;
        bulletDamage = 1f;
        powImage.SelectPowerUp = 0;
    }

    IEnumerator Shooting() {
        isShooting = true;

        if (powerUp == PowerUps.BIG)
        {
            yield return new WaitForSeconds(0.5f);
        }
        else if(powerUp == PowerUps.FAST){
            yield return new WaitForSeconds(0.1f);
        }
        else
        {
            yield return new WaitForSeconds(0.3f);
        }

        if (powerUp == PowerUps.NORMAL || powerUp == PowerUps.FAST)
        {
            Instantiate(bullet, bulletPoint.position, Quaternion.identity);
        }
        else if (powerUp == PowerUps.DOUBLE)
        {
            Instantiate(bullet, bulletPoint.position, Quaternion.identity);
            
        }
        else if (powerUp == PowerUps.BIG)
        {
            Instantiate(bullet, bulletPoint.position, Quaternion.identity);
        }
        isShooting = false;
    }


    IEnumerator Feedback()
    {
        isTakingDamage = true;
        anim.SetBool("isTakingDamage", true);

        yield return new WaitForSeconds(1.5f);

        isTakingDamage = false;
        anim.SetBool("isTakingDamage", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheckPoint.position, checkRadius);
    }
}
