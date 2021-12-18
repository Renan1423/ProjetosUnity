using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Status")]

    [SerializeField] public static int maxLife = 10;
    [SerializeField] public static int life = 10;


    [Header("Movement")]
    [SerializeField] private float speed;
    private float initialSpeed;
    private Vector2 _direction;

    [Header("Rolling")]

    [SerializeField] private float rollSpeed;
    [SerializeField] private float rollTime;
    [SerializeField] private float lastTime = -1f;
    [SerializeField] private float tapSpeed = 0.5f;
    private int lastDirection;

    private Rigidbody2D rig;

    private bool _isRolling;

    [Header("Attack")]
    //Shooting
    private bool isShooting;
    public GameObject[] projectilePrefab;
    public float projectileSpeed;
    public float shootingDelay;
    public static int selectedProjectile;
    private Vector2 bulletDirection; //shoot the bullet depending on the Player's direction

    //Close Combat
    private bool isAttacking;
    public GameObject AtkPoint;
    

    // Get and Set the direction and isRunning values
    public Vector2 direction {
        get { return _direction; }
        set { _direction = value; }
    }

    public bool isRolling
    {
        get { return _isRolling; }
        set { _isRolling = value; }
    }

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();

        initialSpeed = speed;

        bulletDirection = new Vector2(0,1);
    }

    void Update()
    {
        //Movement
        OnInput();

        OnRoll();
        
        //Attack
        OnShoot();

        OnAttack();
    }

    void FixedUpdate()
    {
        //Physics
        OnMove();
    }

    #region Movement
    void OnInput()
    {
        if (!isRolling)
        {
            _direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (_direction != new Vector2(0, 0))
            {
                bulletDirection = _direction;
            }
        }
    }

    void OnMove()
    {
        if (!isAttacking)
        {
            rig.MovePosition(rig.position + _direction * speed * Time.fixedDeltaTime);
        }
    }

    #endregion

    #region Rolling

    void OnRoll() {

        //Double Tap

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {

            float timeSinceLastTap = Time.time - lastTime;

            if (timeSinceLastTap < tapSpeed && lastDirection == 0)
            {
                isRolling = true;
            }
            else
            {
                lastDirection = 0; //moved to the right
            }
            lastTime = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            float timeSinceLastTap = Time.time - lastTime;

            if (timeSinceLastTap < tapSpeed && lastDirection == 1)
            {
                isRolling = true;
            }
            else
            {
                lastDirection = 1; //moved to the left
            }
            lastTime = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {

            float timeSinceLastTap = Time.time - lastTime;

            if (timeSinceLastTap < tapSpeed && lastDirection == 2)
            {
                isRolling = true;
            }
            else
            {
                lastDirection = 2; //moved to the top
            }
            lastTime = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {

            float timeSinceLastTap = Time.time - lastTime;

            if (timeSinceLastTap < tapSpeed && lastDirection == 3)
            {
                isRolling = true;
            }
            else
            {
                lastDirection = 3; //moved to the bottom
            }
            lastTime = Time.time;
        }

        //Roll

        if (isRolling)
        {
            speed = rollSpeed;
            StartCoroutine(RollingCoroutine());
        }
        else {
            speed = initialSpeed;
        }
    }

    IEnumerator RollingCoroutine()
    {
        yield return new WaitForSeconds(rollTime);


        isRolling = false;
    }

    #endregion

    #region Attack

    //Shooting projectiles

    void OnShoot() {
        if (Input.GetKey(KeyCode.X) && !isShooting) {
            
            StartCoroutine(ShootingCoroutine());
        }
    }

    IEnumerator ShootingCoroutine() {
        isShooting = true;

        yield return new WaitForSeconds(shootingDelay);

        GameObject PJ = Instantiate(projectilePrefab[selectedProjectile], transform.position, Quaternion.identity) as GameObject;
        PJ.GetComponent<Rigidbody2D>().velocity = bulletDirection * projectileSpeed;

        isShooting = false;

    }

    //Physical / Close Combat Attack
    void OnAttack()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            isAttacking = true;
            AtkPoint.SetActive(true);
        }

        if (isAttacking)
        {
            StartCoroutine(FinishAttack());
        }
    }

    IEnumerator FinishAttack()
    {
        yield return new WaitForSeconds(0.6f);

        AtkPoint.SetActive(false);
        isAttacking = false;
        StopCoroutine(FinishAttack());
    }

    #endregion
}
