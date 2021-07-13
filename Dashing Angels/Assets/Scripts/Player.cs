using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public float speed;
    public float stopDistance;
    public int life;

    private float timeAttack;
    public float startTimeAttack;

    private float timeWaveAttack;

    private float slowCurrentAmount = 0f;
    public float slowMaxAmount;
    public float slowTimeSpeed;

    private bool isGrounded;
    private bool isAttacking;
    private bool isAttackingW;
    private bool isLimitedUp;
    private bool isLimitedLR;
    private float initialspeed;
    private bool isMoving;

    private Rigidbody2D rig;
    private Animator anim;
    private SpriteRenderer sprite;

    public LayerMask enemyLayer;

    Transform limitLeft;
    Transform limitRight;

    public Transform atkPoint;
    public Transform atkPointL;
    public float atkRadius;

    public Transform point;
    public Transform backPoint;
    public GameObject wave;
    public GameObject windWave;

    public Image[] hearts;

    public GameObject vCam;
    public GameObject lightChange;

    public GameObject skills;

    public GameObject enemies;

    public SceneController sc;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        initialspeed = speed;
        limitLeft = GameObject.FindGameObjectWithTag("LimitLeft").transform;
        limitRight = GameObject.FindGameObjectWithTag("LimitRight").transform;
        for (int i = 0; i < hearts.Length; i++) {
            if (i < life)
            {
                hearts[i].enabled = true;
            }
            else {
                hearts[i].enabled = false;
            }
        }

    }

    void FixedUpdate()
    {
        float distanceLeft = Vector2.Distance(transform.position, limitLeft.position);
        float distanceRight = Vector2.Distance(transform.position, limitRight.position);

        if (distanceLeft <= stopDistance || distanceRight <= stopDistance) {
            rig.velocity = new Vector2(0, rig.velocity.y);
            anim.SetBool("isWalkingH", false);
            isMoving = false;
        }

        if (Input.GetKey(KeyCode.LeftArrow) && distanceLeft > stopDistance && !isMoving && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && !isAttacking)
        {
            isMoving = true;
            speed = initialspeed + 5f;
            rig.velocity = new Vector2(-speed, rig.velocity.y);
            anim.SetBool("isWalkingH", true);
            sprite.flipX = true;
            anim.SetBool("isAttacking", false);
            isAttacking = false;
        }
        //else {
        //    rig.velocity = new Vector2(0, rig.velocity.y);
        //    anim.SetBool("isWalkingH", false);
        //}
        if (Input.GetKey(KeyCode.RightArrow) && distanceRight > stopDistance && !isMoving && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && !isAttacking)
        {
            isMoving = true;
            speed = initialspeed + 5f;
            rig.velocity = new Vector2(speed, rig.velocity.y);
            anim.SetBool("isWalkingH", true);
            sprite.flipX = false;
            anim.SetBool("isAttacking", false);
            isAttacking = false;
        }
        if (Input.GetKey(KeyCode.UpArrow) && isGrounded && !isMoving && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.DownArrow) && !isAttacking)
        {
            isMoving = true;
            speed = initialspeed;
            isGrounded = false;
            rig.velocity = new Vector2(rig.velocity.x, speed);
            anim.SetBool("isWalkingV", true);
            anim.SetBool("isAttacking", false);
            isAttacking = false;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && !isGrounded && !isMoving && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.LeftArrow) && !isAttacking)
        {
            isMoving = true;
            speed = initialspeed;
            rig.velocity = new Vector2(rig.velocity.x, -speed);
            anim.SetBool("isFlying", true);
            anim.SetBool("isAttacking", false);
            isAttacking = false;
        }

        if (isGrounded)
        {
            anim.SetBool("isFlying", false);
            anim.SetBool("isWalkingV", false);
        }
        else {
            anim.SetBool("isFlying", true);
        }

        if (!isLimitedUp)
        {
            anim.SetBool("isFlying", false);
        }
        else
        {
            anim.SetBool("isFlying", true);
            anim.SetBool("isWalkingV", false);
            isMoving = false;
            rig.gravityScale = 0;
        }

        if (isLimitedLR)
        {
            anim.SetBool("isWalkingH", false);
            if (!isGrounded) {
                anim.SetBool("isFlying", true);
            }
            speed = 0;
            isMoving = false;
        }
        


    }

    void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < life)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
        if (timeAttack <= 0)
        {
            if (Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.Alpha1) || Input.GetKey(KeyCode.Alpha3))
            {
                anim.SetBool("isAttacking", true);
                isAttacking = true;
                timeAttack = startTimeAttack;
                Attack();

            }

        }
        else
        {
            timeAttack -= Time.deltaTime;
            anim.SetBool("isAttacking", false);
            isAttacking = false;
        }

        //if (timeWaveAttack <= -0.6f) {
            if (Input.GetKeyDown(KeyCode.Alpha1) && Skills.skill1Life >= Skills.maxSkillLife && !isMoving)
            {
                AttackWave();
                skills.GetComponent<Skills>().FirstSkill();
                //timeWaveAttack = startTimeAttack;
            }
        //}
        //else
        //{
        //    timeWaveAttack -= Time.deltaTime;
        //}
        if (Input.GetKeyDown(KeyCode.Alpha2) && Skills.skill2Life >= Skills.maxSkillLife && !isMoving)
        {
            skills.GetComponent<Skills>().SecondSkill();
            
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && Skills.skill3Life >= Skills.maxSkillLife && !isMoving)
        {
            skills.GetComponent<Skills>().ThirdSkill();
            AttackWindWave();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) && Skills.skill4Life >= Skills.maxSkillLife && !isMoving)
        {
            skills.GetComponent<Skills>().FourthSkill();
        }

        if (Input.GetKeyDown(KeyCode.Alpha5) && Skills.skill5Life >= Skills.maxSkillLife && !isMoving)
        {

            if (Time.timeScale == 1.0f) {
                Time.timeScale = slowTimeSpeed;
                speed += 5f;
                skills.GetComponent<Skills>().FifthSkill();
            }
            else {

                Time.timeScale = 1.0f;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
                lightChange.GetComponent<LightController>().SlowModeColorOFF();
                speed = initialspeed;
            }
        }

        if (Time.timeScale == slowTimeSpeed)
        {
            slowCurrentAmount += Time.deltaTime;
            lightChange.GetComponent<LightController>().SlowModeColorON();
        }

        if (slowCurrentAmount > slowMaxAmount)
        {

            slowCurrentAmount = 0f;
            Time.timeScale = 1.0f;
            lightChange.GetComponent<LightController>().SlowModeColorOFF();

        }
    }

    void Attack() {
            Collider2D hit = Physics2D.OverlapCircle(atkPoint.position, atkRadius,enemyLayer);
            Collider2D hitLeft = Physics2D.OverlapCircle(atkPointL.position, atkRadius, enemyLayer);

            if (hit != null) {
                hit.GetComponent<Enemy>().OnHit();
            }

            if (hitLeft != null)
            {
                hitLeft.GetComponent<Enemy>().OnHit();
            }
    }

    void AttackWave() {
        GameObject sonicwave = Instantiate(wave);
        if (!sprite.flipX)
        {
            sonicwave.transform.position = point.transform.position;
        }
        else {
            sonicwave.transform.position = backPoint.transform.position;
        }
    }

    void AttackWindWave()
    {
        GameObject windwave = Instantiate(windWave);
        if (!sprite.flipX)
        {
            windwave.transform.position = point.transform.position;
        }
        else
        {
            windwave.transform.position = backPoint.transform.position;
        }
    }

    public void OnHit(int damage)
    {
        life-=damage;
       // vCam.GetComponent<CameraController>().CameraShake();
        GameOver();
    }

    void GameOver()
    {
        if (life <= 0)
        {
            sc.GameOver();
            skills.GetComponent<Skills>().FirstSkill();
            skills.GetComponent<Skills>().SecondSkill();
            skills.GetComponent<Skills>().ThirdSkill();
            skills.GetComponent<Skills>().FourthSkill();
            skills.GetComponent<Skills>().FifthSkill();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(atkPoint.position, atkRadius);
        Gizmos.DrawWireSphere(atkPointL.position, atkRadius);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isGrounded = true;
        }
        else {
            isGrounded = false;
        }

        if (collision.gameObject.layer == 9)
        {
            isLimitedUp = true;
        }
        else
        {
            isLimitedUp = false;
        }

        if (collision.gameObject.layer == 10)
        {
            isLimitedLR = true;
        }
        else
        {
            isLimitedLR = false;
        }
    }
}
