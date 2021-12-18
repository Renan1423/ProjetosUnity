using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Skeleton : MonoBehaviour
{
    public Animator EnemyGFX;
    private AIPath AIPath;
    private AIDestinationSetter AIDestinationSetter;
    private Collider2D col;

    [Header("Status")]
    public float HP;
    public float maxHP;
    public float speed;
    public GameObject MiniBarPlace;
    public GameObject MiniBar;
    private bool isDead;

    [Header("Attack")]

    public float Damage;
    public float DelayAttack;
    public float AttackDistanceRadius;
    public LayerMask whatIsEnemy;
    private bool isAttacking;
    private bool AttackCooldown;

    [Header("Follow Target")]

    public float distanceRadius;
    private GameObject Playerr;

    public bool isTargeting;


    void Start()
    {
        transform.SetParent(null);

        AIPath = GetComponent<AIPath>();
        AIDestinationSetter = GetComponent<AIDestinationSetter>();
        col = GetComponent<Collider2D>();

        HP = maxHP + Player.SkeletonHPBonus;

        AIPath.maxSpeed = 0;

        Playerr = FindObjectOfType<Player>().gameObject;

        isDead = false;

        Destroy(gameObject, maxHP);
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

        if (HP <= 0)
        {
            //IsDead

            MiniBarPlace.SetActive(false);
            isDead = true;
            col.isTrigger = true;
            DelayAttack = 0;
            isAttacking = false;
            AIPath.maxSpeed = 0;
            AIPath.maxSpeed = 0;
            StartCoroutine(OnDead());
        }

        if (isDead)
        {
            StartCoroutine(OnDead());
        }

        if (AIDestinationSetter.target == null)
        {
            isTargeting = false;
        }


    }

    void Target()
    {
        #region Targeting

        if (AIDestinationSetter.target != null)
        {
            float Distance = Vector3.Distance(AIDestinationSetter.target.transform.position, transform.position);
            if (Distance < distanceRadius)
            {
                AIPath.maxSpeed = (speed + Player.SkeletonSpdBonus);

            }
            else
            {
                AIPath.maxSpeed = 0;
            }
        }

        #endregion
    }

    void Attack()
    {
        #region Attack

        float Distance = Vector3.Distance(AIDestinationSetter.target.transform.position, transform.position);

        if (Distance < AttackDistanceRadius)
        {
            if (!AttackCooldown)
            {
                isAttacking = true;
                StartCoroutine(OnAttack());
                AttackCooldown = true;
            }

            if (isAttacking)
            {
                AIPath.maxSpeed = 0;
                EnemyGFX.SetInteger("Transition", 1);
            }
            else
            {
                AIPath.maxSpeed = (speed + Player.SkeletonSpdBonus);
                EnemyGFX.SetInteger("Transition", 0);
            }
        }
        else {
            isAttacking = false;
        }

        
        #endregion
    }

    IEnumerator OnAttack()
    {
        StartCoroutine(AIDestinationSetter.target.gameObject.GetComponent<Enemy>().TakeDamage(Damage + Player.SkeletonAtkBonus));

        yield return new WaitForSeconds(DelayAttack);

        AttackCooldown = false;
    }

    IEnumerator OnDead()
    {

        EnemyGFX.SetInteger("Transition", 3);

        yield return new WaitForSeconds(1.5f);

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

            AIPath.maxSpeed = (speed + Player.SkeletonSpdBonus);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, AttackDistanceRadius);

        Gizmos.DrawWireSphere(transform.position, distanceRadius);
    }
}
