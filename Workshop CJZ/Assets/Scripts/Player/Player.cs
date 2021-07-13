using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Attributes")]
    public int health;
    public float speed;
    public float jumpForce;
    public float atkRadius;
    public float recoveryTime;
   

    bool isJumping;
    bool isAttacking;
    bool isDead;

    float recoveryCount;

    [Header("Components")]
    public Rigidbody2D rig;
    public Animator anim;
    public Transform firepoint;
    public LayerMask enemyLayer;
    public Image healthBar;
    public GameController gc;
    

    [Header("Audio Settings")]
    public AudioClip sfx;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            Jump();
            OnAttack();
        }
    }

    void FixedUpdate()
    {
        if (isDead == false)
        {
            OnMove();
        }
    }

    void OnMove() {
        float direcao = Input.GetAxis("Horizontal");

        rig.velocity = new Vector2(direcao * speed, rig.velocity.y);

        if (direcao > 0 && !isJumping && !isAttacking)
        {
            transform.eulerAngles = new Vector2(0, 0);
            anim.SetInteger("Transition", 1);
        }
        if (direcao < 0 && !isJumping && !isAttacking)
        {
            transform.eulerAngles = new Vector2(0, 180);
            anim.SetInteger("Transition", 1);
        }

        if (direcao == 0 && !isJumping && !isAttacking)
        {
            anim.SetInteger("Transition", 0);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (isJumping == false)
            {
                anim.SetInteger("Transition", 2);
                rig.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                isJumping = true;
            }
        }
    }

    void OnAttack() {
        if (Input.GetButtonDown("Fire1")) {
            if (!isAttacking)
            {
                isAttacking = true;
                anim.SetInteger("Transition", 3);
                audioSource.PlayOneShot(sfx);

                Collider2D hit = Physics2D.OverlapCircle(firepoint.position, atkRadius, enemyLayer);

                if (hit != null) {
                    hit.GetComponent<FlightEnemy>().OnHit();
                }

                StartCoroutine(OnAttacking());
            }
        }
    }

    IEnumerator OnAttacking() {
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(firepoint.position, atkRadius);
    }

    public void OnHit(int damage)
    {
        recoveryCount += Time.deltaTime;

        if (recoveryCount >= recoveryTime && isDead == false) {

            healthBar.fillAmount -= (float)damage/(float)health;

            anim.SetTrigger("hit");
            health-=damage;

            GameOver();

            recoveryCount = 0f;
        }
    }

    void GameOver() {
        if (health <= 0) {
            anim.SetTrigger("death");
            isDead = true;
            gc.ShowGameOver();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8) {
            isJumping = false;
        }    
    }
}
