using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private Rigidbody2D rig;
    private Animator anim;
    private SpriteRenderer spr;

    private bool isWalking;
    private bool isAttacking;
    public bool isTakingDamage;
    private bool isDead;

    [Header("Atributos")]
    [SerializeField]

    public float speed;
    public float life;
    public float maxLife;
    public float distanceX;
    public float atkDelay;

    private bool distX;
    private bool distY;
    private float atksCount;

    public float dmgTime = 0.1f;
    public float dmgMaxTime = 0.1f;

    [Header("GameObjects")]
    [SerializeField]

    public GameObject AtkPrefab;
    public GameObject AtkFlipXPrefab;
    public Transform Player;

    public Transform AtkPointLeft;
    public Transform AtkPointRight;

    public GameObject Shadow;

    [Header("Particles")]
    [SerializeField]

    public ParticleSystem DamageParticle;
    private ParticleSystem.EmissionModule damageEmission;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();

        life = maxLife;

        Player = GameObject.FindGameObjectWithTag("Player").transform;

        damageEmission = DamageParticle.emission;
        damageEmission.rateOverTime = 0f;
    }


    void Update()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        
        if (rig.velocity == new Vector2(0, 0) && !isTakingDamage && !isAttacking && !isDead) {
            anim.SetInteger("Action", 0);
            isWalking = false;
        }

        if (!isDead)
        {
            OnWalk();
            StartCoroutine(OnDamage());
        }
        else {
            anim.SetInteger("Action", 4);
            isWalking = false;
        }
        OnDead();

    }

    void FixedUpdate()
    {
        if (!isDead)
        {
            OnAttack();
        }
    }

    void OnWalk() {

        float actualDistanceX = rig.transform.position.x - Player.transform.position.x;
        float actualDistanceY = rig.transform.position.y - Player.transform.position.y;

        if (actualDistanceX < -25 || actualDistanceX > 25)
        {

        }
        else {
            if (actualDistanceX > 0)
            {
                spr.flipX = false;
            }
            else
            {
                spr.flipX = true;
            }

            if (actualDistanceX < distanceX && actualDistanceX > -distanceX)
            {
                distX = true;
            }
            else
            {
                distX = false;
            }

            if (actualDistanceY < 0.3 && actualDistanceY > -0.3)
            {
                distY = true;
            }
            else
            {
                distY = false;
            }

            if (!isAttacking && !isTakingDamage && !isDead)
            {

                if (actualDistanceX > distanceX)
                {
                    rig.velocity = new Vector2(-speed, rig.velocity.y);
                    spr.flipX = false;
                    anim.SetInteger("Action", 1);
                    distX = false;
                    isAttacking = false;
                    isWalking = true;
                }
                else if (actualDistanceX < -distanceX)
                {
                    rig.velocity = new Vector2(speed, rig.velocity.y);
                    spr.flipX = true;
                    anim.SetInteger("Action", 1);
                    distX = false;
                    isAttacking = false;
                    isWalking = true;
                }
                else
                {
                    rig.velocity = new Vector2(0, rig.velocity.y);
                    distX = true;
                }

                if (actualDistanceY > 0.3)
                {
                    rig.velocity = new Vector2(rig.velocity.x, -speed);
                    anim.SetInteger("Action", 1);
                    distY = false;
                    isAttacking = false;
                    isWalking = true;
                }
                else if (actualDistanceY < -0.3)
                {
                    rig.velocity = new Vector2(rig.velocity.x, speed);
                    anim.SetInteger("Action", 1);
                    distY = false;
                    isAttacking = false;
                    isWalking = true;
                }
                else
                {
                    rig.velocity = new Vector2(rig.velocity.x, 0);
                    distY = true;
                }

            }

            if (distX && distY)
            {
                isAttacking = true;
                isWalking = false;
            }
            else
            {
                isAttacking = false;
                isWalking = true;
            }
        }
       
    }

    void OnAttack() {

        if (!isAttacking) {
            return;
        }
        else if (atksCount != 0 && isAttacking)
        {
            StartCoroutine(CountsZero());
        }

        if (isAttacking && !isWalking && !isTakingDamage && atksCount == 0 && distX && distY) {
            StartCoroutine(Attack());
        }
        //else if (atksCount != 0 && !isAttacking)
        //{
        //    StartCoroutine(CountsZero());
        //}
    }

    IEnumerator Attack()
    {
        if (atksCount == 0)
        {
            anim.SetInteger("Action", 2);
        }

        yield return new WaitForSeconds(atkDelay);

        if (atksCount == 0)
        {
            if (spr.flipX == true)
            {
                Instantiate(AtkFlipXPrefab, AtkPointRight.position, Quaternion.identity);
            }
            else
            {
                Instantiate(AtkPrefab, AtkPointLeft.position, Quaternion.identity);
            }
            atksCount++;
            yield return new WaitForSeconds(1f);
        }
        else {
            yield return new WaitForSeconds(2f);
            if (!isDead)
            {
                anim.SetInteger("Action", 0);
            }
            else {
                anim.SetInteger("Action", 4);
            }
            isAttacking = false;
        }

        isAttacking = false;

        yield return new WaitForSeconds(1f);

        if (!isDead)
        {
            anim.SetInteger("Action", 0);
        }
        else
        {
            anim.SetInteger("Action", 4);
        }

    }

    IEnumerator CountsZero() {

        yield return new WaitForSeconds(1f);

        if (atksCount > 0)
        {
            atksCount -= Time.deltaTime;
        }
        else if (atksCount < 0) {
            atksCount = 0;
        }
        
    }

    public IEnumerator OnDamage() {
        if (dmgTime < dmgMaxTime) {
            dmgTime += Time.deltaTime;
        }

        if (isTakingDamage)
        {

            if(dmgTime >= dmgMaxTime) {
                life--;
                int n = Random.Range(0, 10);
                if (n <= 8)
                {
                    AudioController.current.PlayMusic(AudioController.current.punch);
                }
                else
                {
                    AudioController.current.PlayMusic(AudioController.current.punchHeavy);
                }
                damageEmission.rateOverTime = 10f;
                dmgTime = 0;
            }
            if (!isDead)
            {
                anim.SetInteger("Action", 3);
            }
            else {
                anim.SetInteger("Action", 4);
                isTakingDamage = false;
            }
            yield return new WaitForSeconds(0.3f);

            damageEmission.rateOverTime = 0f;
            isTakingDamage = false;
            if (!isDead){
                anim.SetInteger("Action", 0);
            }
            else
            {
                anim.SetInteger("Action", 4);
            }

        }
    }

    void OnDead() {
        if (life <= 0) {
            Destroy(Shadow);
            anim.SetInteger("Action", 4);
            rig.velocity = new Vector2(0, 0);
            isDead = true;
            isTakingDamage = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(DestroyEnemy());
        }

    }

    IEnumerator DestroyEnemy() {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        

    }

    

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerAttack") {
            anim.SetInteger("Action", 3);
        }
    }

}
