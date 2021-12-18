using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy : MonoBehaviour
{
    public Animator EnemyGFX;
    private AIPath AIPath;
    private AIDestinationSetter AIDestinationSetter;
    private Rigidbody2D rig;
    private Collider2D col;

    private EnemyCount enemyCount;

    [Header("Status")]
    public float HP;
    public float maxHP;
    public float speed;
    public GameObject MiniBarPlace;
    public GameObject MiniBar;
    private bool isDead;

    public bool hasWarriorBonus;
    public bool hasArcherBonus;
    public bool hasMageBonus;
    public bool hasNinjaBonus;

    public float normalBonus;
    public float specialBonus;

    [Header("Attack")]

    public float Damage;
    public float DelayAttack;
    public float AttackDistanceRadius;
    public LayerMask whatIsPlayer;
    public LayerMask whatIsSkeleton;
    public LayerMask whatIsEnemy;
    private bool isAttacking;
    private bool AttackCooldown;

    [Header("Follow Target")]

    public float distanceRadius;

    private GameObject Player;

    [Header("Archer")]

    public bool isArcher;
    public GameObject ArrowPrefab;
    public float ArrowSpeed;



    private bool Voiced = false;


    void Start()
    {

        specialBonus = (50 + specialBonus) - EnemyCount.floor;
        normalBonus = (50 + normalBonus) - EnemyCount.floor;

        transform.SetParent(null);

        enemyCount = FindObjectOfType<EnemyCount>();

        AIPath = GetComponent<AIPath>();
        AIDestinationSetter = GetComponent<AIDestinationSetter>();
        rig = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        if (hasWarriorBonus)
        {
            maxHP = maxHP + (specialBonus);
        }
        else {
            maxHP = maxHP + (normalBonus);
        }

        HP = maxHP;

        AIPath.maxSpeed = 0;

        Player = FindObjectOfType<Player>().gameObject;

        AIDestinationSetter.target = Player.transform;

        isDead = false;
    }

    void Update()
    {
        MiniBar.GetComponent<Transform>().localScale = new Vector3(HP / maxHP, 1f, 1f);

        if (!isDead)
        {
            Attack();

            Target();
        }

        if (HP < maxHP && HP > 0)
        {
            MiniBarPlace.SetActive(true);
        }

        if (HP <= 0) {
            //IsDead

            if (!isDead)
            {
                enemyCount = FindObjectOfType<EnemyCount>();
                enemyCount.GetComponent<EnemyCount>().numberOfEnemies--;
            }
            MiniBarPlace.SetActive(false);
            isDead = true;
            col.isTrigger = true;
            DelayAttack = 0;
            isAttacking = false;
            AIPath.maxSpeed = 0;
            StartCoroutine(OnDead());
        }

        if (isDead) {
            StartCoroutine(OnDead());
        }

        
    }

    void Target() {
        #region Targeting
        //Skeletons = FindObjectsOfType<Skeletons>();

        float Distance = Vector3.Distance(AIDestinationSetter.target.transform.position, transform.position);

        if (!isArcher)
        {
            if (Distance < distanceRadius)
            {
                if (hasNinjaBonus)
                {
                    AIPath.maxSpeed = (speed + specialBonus);
                }
                else
                {
                    AIPath.maxSpeed = (speed + normalBonus);
                }

                //AIDestinationSetter.target = Skeletons[0].transform;

                Distance = Vector3.Distance(AIDestinationSetter.target.transform.position, transform.position);

                /*if (Distance < distanceRadius)
                {
                    AIPath.maxSpeed = speed;
                }
                else
                {
                    AIDestinationSetter.target = Player.transform;
                }*/
            }
            else
            {
                AIPath.maxSpeed = 0;
            }
        }
        else {
            if (Distance < distanceRadius && Distance > AttackDistanceRadius)
            {
                if (hasNinjaBonus)
                {
                    AIPath.maxSpeed = (speed + specialBonus);
                }
                else
                {
                    AIPath.maxSpeed = (speed + normalBonus);
                }

                //AIDestinationSetter.target = Skeletons[0].transform;

                Distance = Vector3.Distance(AIDestinationSetter.target.transform.position, transform.position);

                /*if (Distance < distanceRadius)
                {
                    AIPath.maxSpeed = speed;
                }
                else
                {
                    AIDestinationSetter.target = Player.transform;
                }*/
            }
            else if(Distance < distanceRadius && Distance <= AttackDistanceRadius)
            {
                AIPath.maxSpeed = 0;
            }

            if (Distance > distanceRadius) {
                AIPath.maxSpeed = 0;
            }
        }

        #endregion
    }

    void Attack() {
        #region Attack

        if (AIDestinationSetter.target.tag == "Player")
        {
            isAttacking = Physics2D.OverlapCircle(transform.position, AttackDistanceRadius, whatIsPlayer);
        }
        else if (AIDestinationSetter.target.tag == "Skeleton")
        {
            isAttacking = Physics2D.OverlapCircle(transform.position, AttackDistanceRadius, whatIsSkeleton);
        }
        else if (AIDestinationSetter.target.tag == "Enemy")
        {
            isAttacking = Physics2D.OverlapCircle(transform.position, AttackDistanceRadius, whatIsEnemy);
        }

        if (!isArcher)
        {
            if (isAttacking && !AttackCooldown)
            {
                StartCoroutine(OnAttack());
                AttackCooldown = true;
            }

        }

        else
        {
            if (isAttacking && !AttackCooldown)
            {
                StartCoroutine(OnAttackArrow());
                AttackCooldown = true;
            }
        }

        if (isAttacking)
        {
            AIPath.maxSpeed = 0;
            EnemyGFX.SetInteger("Transition", 1);
        }
        else
        {
            if (hasNinjaBonus)
            {
                AIPath.maxSpeed = (speed + specialBonus);
            }
            else
            {
                AIPath.maxSpeed = (speed + normalBonus);
            }
            EnemyGFX.SetInteger("Transition", 0);
        }
        #endregion
    }

    IEnumerator OnAttack() {
        if (AIDestinationSetter.target.tag == "Player")
        {
            StartCoroutine(AIDestinationSetter.target.gameObject.GetComponent<Player>().TakeDamage(Damage + normalBonus));
        }
        else {
            StartCoroutine(AIDestinationSetter.target.gameObject.GetComponent<Skeleton>().TakeDamage(Damage + normalBonus));
        }

        yield return new WaitForSeconds(DelayAttack);

        AttackCooldown = false;
    }

    IEnumerator OnAttackArrow() {
        Vector3 difference = AIDestinationSetter.target.transform.position - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        float distance = difference.magnitude;
        Vector2 direction = difference / distance;
        direction.Normalize();
        ShootArrow(direction, rotationZ);

        yield return new WaitForSeconds(DelayAttack);

        AttackCooldown = false;
    }

    void ShootArrow(Vector2 direction, float rotationZ) {
        GameObject Arrow = Instantiate(ArrowPrefab) as GameObject;
        Arrow.transform.position = transform.position;
        Arrow.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        if (hasArcherBonus)
        {
            ArrowSpeed = (ArrowSpeed + normalBonus) + Random.Range(-1f, 2f);
            AudioController.current.PlayMusic(AudioController.current.arrowSFX);
        }
        else {
            ArrowSpeed = (ArrowSpeed) + Random.Range(-1f, 2f);
            AudioController.current.PlayMusic(AudioController.current.spellSFX);
        }
        Arrow.GetComponent<Rigidbody2D>().velocity = direction * ArrowSpeed;
        if (hasMageBonus)
        {
            Arrow.GetComponent<Projectiles>().Damage += specialBonus;
        }
        else {
            Arrow.GetComponent<Projectiles>().Damage += normalBonus;
        }

    }

    IEnumerator OnDead() {

        EnemyGFX.SetInteger("Transition", 3);

        yield return new WaitForSeconds(1.5f);

        /*if (!Voiced)
        {
            int voice = Random.Range(0, 100);
            if (voice < 40 && !Voiced)
            {
                AudioController.current.PlayMusic(AudioController.current.enemyDeadSFX);
                Voiced = false;
            }
        }*/

        //Destroy(gameObject);
    }

    public IEnumerator TakeDamage(float dmg)
    {
        if (!isDead)
        {
            HP -= dmg;

            AIPath.maxSpeed = 0;

            EnemyGFX.SetInteger("Transition", 2);

            yield return new WaitForSeconds(1f);

            EnemyGFX.SetInteger("Transition", 0);

            if (hasNinjaBonus)
            {
                AIPath.maxSpeed = (speed + specialBonus);
            }
            else {
                AIPath.maxSpeed = (speed + normalBonus);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, AttackDistanceRadius);

        Gizmos.DrawWireSphere(transform.position, distanceRadius);
    }
}
