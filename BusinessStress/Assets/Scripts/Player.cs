using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody2D rig;
    private Animator anim;
    private SpriteRenderer spr;

    public float life;
    public float maxLife;
    public int numLifes;

    public int dmgTaken;

    public bool isCutscene;

    [Header("Movimento")]
    [SerializeField]

    public float speed = 5;
    public float runSpeed = 7;
    public float carrySpeed = 3;
    private float initialSpeed;
    private float initialRunSpeed;
    public float lastTime = -1f;
    public float tapSpeed = 0.5f;
    private bool isMoving;
    private bool isRunning;

    public float dodgeSpeedX;
    public float dodgeSpeedY;

    [Header("Ações")]
    [SerializeField]

    private bool isDefending;
    private bool isDodging;
    private bool isCharging;
    private bool isAttacking;
    public static bool isHeavyAttacking;
    public bool isTakingDamage;
    private bool isDead;
    private bool Charged;
    public float chargeTime = 0;
    public float chargeMaxTime = 1;

    public float dmgTime = 2f;
    public float dmgMaxTime = 2f;

    private bool GameOver;

    [Header("GameObjects")]
    [SerializeField]

    public GameObject FirePrefab;
    public GameObject MiniFirePrefab;
    private GameObject MiniFire;
    public Transform FirePoint;
    public Transform MiniFirePoint;

    public GameObject AtkCollider;
    public GameObject AtkColliderRight;


    private GameObject VCam;

    public GameObject GameOverPanel;

    [Header("Pontuação")]
    [SerializeField]

    public static int Points = 0;

    [Header("Particles")]
    [SerializeField]

    public ParticleSystem FootSteps;
    private ParticleSystem.EmissionModule footEmission;

    public ParticleSystem DamageParticle;
    private ParticleSystem.EmissionModule damageEmission;

    [Header("Carregar Objetos")]
    [SerializeField]

    public LayerMask WhatIsObject;
    public float grabCheckRadius;
    public Transform CheckObjectLeft;
    public Transform CheckObjectRight;
    public Transform HoldPosition;

    private Collider2D ObjectLeft;
    private Collider2D ObjectRight;

    private bool isCarrying;


    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();

        life = maxLife;

        initialSpeed = speed;
        initialRunSpeed = runSpeed;

        VCam = GameObject.FindGameObjectWithTag("MainCamera");
        VCam.GetComponent<CameraController>();

        footEmission = FootSteps.emission;
        damageEmission = DamageParticle.emission;

        damageEmission.rateOverTime = 0f;

        GameOver = false;

    }


    void Update()
    {

        if (!isCutscene && !GameOver)
        {
            if (!isAttacking && !isCharging && !isDead && !isTakingDamage)
            {
                OnMove();
            }
            if (!isDead)
            {
                OnDefend();
                OnDodge();
                OnAttack();
            }

            if (isDead)
            {
                anim.SetInteger("Action", 8);
                rig.velocity = new Vector2(0, 0);
            }

            if (isTakingDamage)
            {
                rig.velocity = new Vector2(0, 0);
            }

            

            OnGrab();
            OnThrow();
        }
        
    }

    void FixedUpdate()
    {
        if (!isCutscene && !GameOver) {
            StartCoroutine(OnDamage());
        }
    }

    void OnMove()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && isDefending == false && isAttacking == false && isCharging == false)
        {
            if (Time.time - lastTime < 0.5f)
            {
                rig.velocity = new Vector2(runSpeed, rig.velocity.y);
                anim.SetInteger("Action", 2);
                if (!isCarrying)
                {
                    footEmission.rateOverTime = 50f;
                }
                isRunning = true;
            }
            else
            {
                lastTime = Time.time;
                rig.velocity = new Vector2(speed, rig.velocity.y);
                anim.SetInteger("Action", 1);
                footEmission.rateOverTime = 0f;
                isRunning = false;
            }
            spr.flipX = true;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            rig.velocity = new Vector2(0, rig.velocity.y);
        }


        if (Input.GetKeyDown(KeyCode.LeftArrow) && isDefending == false && isAttacking == false && isCharging == false)
        {
            if (Time.time - lastTime < 0.5f)
            {
                rig.velocity = new Vector2(-runSpeed, rig.velocity.y);
                anim.SetInteger("Action", 2);
                if (!isCarrying)
                {
                    footEmission.rateOverTime = 50f;
                }
                isRunning = true;
            }
            else
            {
                lastTime = Time.time;
                rig.velocity = new Vector2(-speed, rig.velocity.y);
                anim.SetInteger("Action", 1);
                footEmission.rateOverTime = 0f;
                isRunning = false;
            }
            spr.flipX = false;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            rig.velocity = new Vector2(0, rig.velocity.y);
        }


        if (Input.GetKeyDown(KeyCode.UpArrow) && isDefending == false && isAttacking == false && isCharging == false)
        {
            if (Time.time - lastTime < 0.5f)
            {
                rig.velocity = new Vector2(rig.velocity.x, runSpeed);
                anim.SetInteger("Action", 2);
                if (!isCarrying)
                {
                    footEmission.rateOverTime = 50f;
                }
                isRunning = true;
            }
            else
            {
                lastTime = Time.time;
                rig.velocity = new Vector2(rig.velocity.x, speed);
                anim.SetInteger("Action", 1);
                footEmission.rateOverTime = 0f;
                isRunning = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
            rig.velocity = new Vector2(rig.velocity.x, 0);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && isDefending == false && isAttacking == false && isCharging == false)
        {
            if (Time.time - lastTime < 0.5f)
            {
                rig.velocity = new Vector2(rig.velocity.x, -runSpeed);
                anim.SetInteger("Action", 2);
                if (!isCarrying)
                {
                    footEmission.rateOverTime = 50f;
                }
                isRunning = true;
            }
            else
            {
                lastTime = Time.time;
                rig.velocity = new Vector2(rig.velocity.x, -speed);
                anim.SetInteger("Action", 1);
                footEmission.rateOverTime = 0f;
                isRunning = false;
            }
        }
        if (Input.GetKeyUp(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow))
        {
            rig.velocity = new Vector2(rig.velocity.x, 0);
        }

        if (rig.velocity == new Vector2(0, 0) && isDefending == false && isDodging == false)
        {
            anim.SetInteger("Action", 0);
            isMoving = false;
            footEmission.rateOverTime = 0f;
        }
        else {
            isMoving = true;
        }
    }

    void OnDefend() {
        if (Input.GetKeyDown(KeyCode.A) && isAttacking == false) {
            isDefending = true;
            anim.SetInteger("Action", 3);
            dmgTime = 0;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            isDefending = false;
            anim.SetInteger("Action", 0);
        }
    }

    void OnDodge() {
        if (isDefending)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) && isDodging == false && isAttacking == false) {
                anim.SetInteger("Action", 5);
                spr.flipX = true;
                dodgeSpeedX = 5;
                dodgeSpeedY = 0;
                StartCoroutine(DodgeAnimation());
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && isDodging == false && isAttacking == false)
            {
                anim.SetInteger("Action", 4);
                spr.flipX = false;
                dodgeSpeedX = -5;
                dodgeSpeedY = 0;
                StartCoroutine(DodgeAnimation());
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) && isDodging == false && isAttacking == false)
            {
                if (spr.flipX == false)
                {
                    anim.SetInteger("Action", 4);
                }
                else {
                    anim.SetInteger("Action", 5);
                }
                dodgeSpeedX = 0;
                dodgeSpeedY = 5;
                StartCoroutine(DodgeAnimation());
            } 
            else if (Input.GetKeyDown(KeyCode.DownArrow) && isDodging == false && isAttacking == false)
            {
                if (spr.flipX == false)
                {
                    anim.SetInteger("Action", 4);
                }
                else {
                    anim.SetInteger("Action", 5);
                }
                dodgeSpeedX = 0;
                dodgeSpeedY = -5;
                StartCoroutine(DodgeAnimation());
            }
        }
    }

    IEnumerator DodgeAnimation(){
        isDodging = true;

        rig.velocity = new Vector2(dodgeSpeedX, dodgeSpeedY);

        yield return new WaitForSeconds(0.45f);

        if (isDefending == true)
        {
            anim.SetInteger("Action", 3);
        }
        else {
            anim.SetInteger("Action", 0);
        }
        isDodging = false;
        rig.velocity = new Vector2(0, 0);
    }

    void OnAttack() {
        if (Input.GetKeyDown(KeyCode.X) && !isDefending && !isDodging && !isMoving && !isCharging && !isDead)
        {
            anim.SetTrigger("Attack");
            StartCoroutine(InitializeAttack());
            isAttacking = true;
        }
        else if(Input.GetKeyUp(KeyCode.X) && isAttacking && !isCharging && !isDead)
        {
            isAttacking = false;
        }

        
        
        if (Input.GetKeyDown(KeyCode.Z) && !isDefending && !isDodging && !isMoving && !isAttacking && !isDead) {
            anim.SetBool("Charging",true);
            isCharging = true;
        }
        if (Input.GetKeyUp(KeyCode.Z) && !isDefending && !isDodging && !isMoving && isCharging && !isAttacking && !isDead)
        {
            if (Charged)
            {
                StartCoroutine(ChargedAttack());
                isCharging = false;
                VCam.GetComponent<CameraController>().CameraLongShake();
                GameObject Fire = Instantiate(FirePrefab);
                Fire.transform.position = FirePoint.position;
                anim.SetTrigger("HeavyAttack");
                StartCoroutine(IsChargingFalse());
            }
            else {
                chargeTime = 0;
                isCharging = false;
                anim.SetBool("Charging", false);
            }
        }



        //Carregando

        if (isCharging) {
            if (chargeTime >= chargeMaxTime)
            {
                Charged = true;
                MiniFire = Instantiate(MiniFirePrefab);
                MiniFire.transform.position = MiniFirePoint.position;
            }
            else {
                chargeTime += Time.deltaTime;
                spr.color = Color.red;
                Object.Destroy(MiniFire);
            }
        }
    }

    IEnumerator InitializeAttack() {

        if (spr.flipX == false)
        {
            AtkCollider.SetActive(true);
        }
        else {
            AtkColliderRight.SetActive(true);
        }

        yield return new WaitForSeconds(0.5f);

        AtkCollider.SetActive(false);
        AtkColliderRight.SetActive(false);
    }

    IEnumerator ChargedAttack()
    {
        isAttacking = true;

        yield return new WaitForSeconds(2.5f);

        isAttacking = false;

    }

    IEnumerator IsChargingFalse()
    {
        isHeavyAttacking = true;


        yield return new WaitForSeconds(1f);


        isHeavyAttacking = false;
        isCharging = false;
        anim.SetBool("Charging", false);
        Charged = false;
        chargeTime = 0;
    }


    public IEnumerator OnDamage()
    {
        if (dmgTime < dmgMaxTime && !isDefending)
        {
            dmgTime += Time.deltaTime;
        }

        if (isTakingDamage && !isDead)
        {
            
            if (dmgTime >= dmgMaxTime)
            {
                life -= dmgTaken;
                damageEmission.rateOverTime = 10f;
                OnDead();
                dmgTime = 0;
            }
            anim.SetInteger("Action", 6);

            yield return new WaitForSeconds(0.05f);

            anim.SetInteger("Action", 6);

            yield return new WaitForSeconds(0.5f);

            isTakingDamage = false;

            damageEmission.rateOverTime = 0f;
            if (rig.velocity == new Vector2(0, 0))
            {
                anim.SetInteger("Action", 0);
            }
            else
            {
                if (isRunning == true)
                {
                    anim.SetInteger("Action", 2);
                }
                else {
                    anim.SetInteger("Action", 1);
                }
                
            }
            
            
        }
    }

    void OnDead() {
        if (life <= 0) {
            isDead = true;
            chargeTime = 0;
            isCharging = false;
            anim.SetBool("Charging", false);
            anim.SetInteger("Action", 8);
            StartCoroutine(Revive());
        }
    }

    IEnumerator Revive() {

        if (numLifes >= 1)
        {
            numLifes--;
            anim.SetInteger("Action", 8);

            yield return new WaitForSeconds(3f);

            life = maxLife;

            isDead = false;

            anim.SetInteger("Action", 0);
        }
        else {
            GameOver = true;
            GameOverPanel.SetActive(true);
        }


    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            life += 4;
            Destroy(collision.gameObject);
        }
    }


    void OnGrab() {
        if (spr.flipX == false)
        {
            ObjectRight = Physics2D.OverlapCircle(CheckObjectLeft.position, grabCheckRadius, WhatIsObject);
        }
        else {
            ObjectRight = Physics2D.OverlapCircle(CheckObjectRight.position, grabCheckRadius, WhatIsObject);
        }

        if (Input.GetKeyDown(KeyCode.S) && ObjectRight != null && !isCarrying)
        {
            //Grab
            isCarrying = true;
            ObjectRight.gameObject.tag = "CarObject";
            ObjectRight.gameObject.transform.position = HoldPosition.transform.position;
            ObjectRight.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 200;
            ObjectRight.gameObject.transform.parent = gameObject.transform;
        }

        if (isCarrying)
        {
            anim.SetBool("Carrying", true);
            speed = carrySpeed;
            runSpeed = carrySpeed;
        }
        else {
            anim.SetBool("Carrying", false);
            speed = initialSpeed;
            runSpeed = initialRunSpeed;
        }
    }

    void OnThrow() {
        if (Input.GetKeyDown(KeyCode.W) && isCarrying) {
            isCarrying = false;
            GameObject carriedObject = GameObject.FindGameObjectWithTag("CarObject");
            carriedObject.GetComponent<AtkCollider>().enabled = true;
            carriedObject.GetComponent<Transform>().parent = null;
            carriedObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            carriedObject.GetComponent<BoxCollider2D>().isTrigger = true;
            if (spr.flipX == false)
            {
                carriedObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-20f, -4f);
            }
            else
            {
                carriedObject.GetComponent<Rigidbody2D>().velocity = new Vector2(20f, -4f);
            }
            carriedObject.transform.parent = null;
            carriedObject.GetComponent<Object>().DestroyThisGameObject();
        }
    }

    void OnDrawGizmos()
    {
        //if (spr.flipX == false) {
        //    Gizmos.DrawWireSphere(CheckObjectLeft.position, grabCheckRadius);
        //}
        //else
        //{
        //    Gizmos.DrawWireSphere(CheckObjectRight.position, grabCheckRadius);
        //}
        
    }
}
