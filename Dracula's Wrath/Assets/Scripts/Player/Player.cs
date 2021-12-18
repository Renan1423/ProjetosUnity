using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{


    [Header("Movement")]

    public float speed;
    public Vector2 direction;
    
    
    private Rigidbody2D rig;
    private Animator anim;

    [Header("Status")]

    static public float HP = 175;
    static public float maxHP = 175;

    static public float MP = 125;
    static public float maxMP = 125;

    public Image HPbar;
    public Image MPbar;

    static public float HPBonus = 0;
    static public float MPBonus = 0;
    static public float SpeedBonus = 0;
    static public float DarkSphereDamageBonus = 0;
    static public float DarkSphereSpeedBonus = 0;
    static public float TeleportCooldownBonus = 0;
    static public float SkeletonHPBonus = 0;
    static public float SkeletonAtkBonus = 0;
    static public float SkeletonSpdBonus = 0;
    static public float AbsorbHPBonus = 0;
    
    

    [Header("Magics")]

    public int magicSelection = 0;

    public GameObject PlayerAim;
    public Transform PlayerArm;

    [SerializeField] private GameObject DarkSpherePrefab;
    [SerializeField] private GameObject TeleportPrefab;
    [SerializeField] private GameObject BarrierPrefab;

    //Dark Sphere
    public float DarkSphereSpeed = 7f;
    private float DarkSphereCharge = 0f;
    public float DarkSphereLimit = 1.2f;
    public GameObject MiniBarPlace;
    public GameObject MiniBar;
    public GameObject MiniBar2;

    //Teleport

    public GameObject TeleportAim;
    public LayerMask whatIsWall;
    public float checkRadius;
    public float TeleportCooldown;
    public float Cooldown = 3;

    //Spawn Skeleton

    public GameObject SkeletonPrefab;
    public float SpawnCooldown;
    public float SpawnMaxCooldown = 2.5f;

    //Drain Blood

    public GameObject BloodDrain;

    //Pause and GameOver System

    public GameObject PausePanel;
    static public bool isPaused;
    public GameObject GameOverPanel;

    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (HPBonus >= 200) {
            HPBonus = 200;
        }

        if (MPBonus >= 150) {
            MPBonus = 150;
        }

        if (SpeedBonus >= 10)
        {
            SpeedBonus = 10;
        }

        if (DarkSphereDamageBonus >= 10)
        {
            DarkSphereDamageBonus = 10;
            DarkSphereSpeedBonus = 2.5f;
        }

        if (TeleportCooldownBonus >= Cooldown) {
            TeleportCooldownBonus = Cooldown;
        }

        if (SkeletonHPBonus >= 20) {
            SkeletonHPBonus = 20;
            SkeletonAtkBonus = 12;
            SkeletonSpdBonus = 6;
        }

        if (AbsorbHPBonus >= 10) {
            AbsorbHPBonus = 10;
        }

        maxHP += HPBonus;
        maxMP += MPBonus;

        //Setting upgrade limits

        
    }


    void Update()
    {
        OnInput();

        OnMagic();

        Health();

        Mana();

        OnPauseGame();
    }

    void FixedUpdate()
    {
        OnMove();    
    }

    void Health() {
        //HPbar.GetComponent<Transform>().localScale = new Vector3(HP / maxHP, 1f, 1f);
        HPbar.fillAmount = HP / maxHP;

        if (HP > maxHP)
        {
            HP = maxHP;
        }

        if (HP < 0) {
            //Game Over!
            isPaused = true;
            GameOverPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    void Mana() {
        //MPbar.GetComponent<Transform>().localScale = new Vector3(MP / maxMP, 1f, 1f);
        MPbar.fillAmount = MP / maxMP;

        if(MP < maxMP)
        {
            MP += Time.deltaTime * 2;
        }

        if (MP > maxMP) {
            MP = maxMP;
        }
    }

    public IEnumerator TakeDamage(float dmg) {
        HP -= dmg;

        anim.SetInteger("Transition", 2);

        yield return new WaitForSeconds(0.3f);

        anim.SetInteger("Transition", 0);
    }

    void OnPauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!isPaused)
            {
                PausePanel.SetActive(true);
                isPaused = true;
                Time.timeScale = 0;

            }
            else {
                Time.timeScale = 1;
                PausePanel.SetActive(false);
                isPaused = false;
            }
        }


    }

    #region Movement

    void OnInput() {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void OnMove()
    {
        rig.MovePosition(rig.position + direction * (speed + SpeedBonus) * Time.fixedDeltaTime);


        if (direction.sqrMagnitude > 0)
        {
            anim.SetInteger("Transition", 1);
        }
        else
        {
            anim.SetInteger("Transition", 0);
        }

        if (direction.x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }

        if (direction.x < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
    }



    #endregion

    #region Magic
    void OnMagic() {
        //Magic Selection

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            magicSelection = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            magicSelection = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            magicSelection = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            magicSelection = 3;
        }

        //Magics

        if (magicSelection == 0)
        {
            MiniBarPlace.SetActive(true);
            TeleportAim.SetActive(false);

            OnDarkSphere();
        }
        else if(magicSelection == 1){
            MiniBarPlace.SetActive(false);
            TeleportAim.SetActive(true);

            OnTeleport();
        }
        else if (magicSelection == 2)
        {
            MiniBarPlace.SetActive(false);
            TeleportAim.SetActive(true);

            OnSummon();
        }
        else if (magicSelection == 3)
        {
            MiniBarPlace.SetActive(false);
            TeleportAim.SetActive(false);

            OnAbsorbBlood();
        }

    }

    #endregion

    #region DarkSphere
    void OnDarkSphere() {
        Vector3 difference = PlayerAim.transform.position - PlayerArm.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        //Dark Sphere

        if (Input.GetMouseButtonDown(0) && MP >= 5)
        {
            float distance = difference.magnitude;
            Vector2 direction = difference / distance;
            direction.Normalize();
            ShootDarkSphere(direction, rotationZ);
            DarkSphereCharge = 0;
        }

        //Charging

        if (Input.GetMouseButton(0))
        {
            MiniBar.SetActive(true);
            DarkSphereCharge += Time.deltaTime;
            if (DarkSphereCharge >= DarkSphereLimit) {
                MiniBar2.SetActive(true);
            }
            if (DarkSphereCharge >= DarkSphereLimit * 2) {
                DarkSphereCharge = DarkSphereLimit * 2;
            }

            if (DarkSphereCharge <= DarkSphereLimit)
            {
                MiniBar.GetComponent<Transform>().localScale = new Vector3(DarkSphereCharge / DarkSphereLimit, 1f, 1f);
            }
            else
            {
                MiniBar2.GetComponent<Transform>().localScale = new Vector3(DarkSphereCharge / (DarkSphereLimit * 2), 1f, 1f);
            }

        }
        else {
            MiniBar.SetActive(false);
            MiniBar2.SetActive(false);
        }

        //Using charged Dark Sphere

        if (Input.GetMouseButtonUp(0) && DarkSphereCharge >= DarkSphereLimit && MP >= 5)
        {
            float distance = difference.magnitude;
            Vector2 direction = difference / distance;
            direction.Normalize();
            ShootDarkSphere(direction, rotationZ);
            DarkSphereCharge = 0;
        }
    }

    void ShootDarkSphere(Vector2 direction, float rotationZ) {
        GameObject DS = Instantiate(DarkSpherePrefab) as GameObject;
        MP -= 5;
        DS.transform.position = PlayerArm.position;
        DS.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        if (DarkSphereCharge >= DarkSphereLimit && DarkSphereCharge < DarkSphereLimit * 2)
        {
            DS.transform.localScale = new Vector3(3f, 3f, 3f);
        }
        else if (DarkSphereCharge >= DarkSphereLimit * 2) {
            DS.transform.localScale = new Vector3(5f, 5f, 5f);
        }
        AudioController.current.PlayMusic(AudioController.current.forcePulseSFX);
        AudioController.current.PlayMusic(AudioController.current.spellSFX);
        DS.GetComponent<Rigidbody2D>().velocity = direction * (DarkSphereSpeed + DarkSphereSpeedBonus);
        DS.GetComponent<Projectiles>().Damage += DarkSphereDamageBonus;
    }
    #endregion

    #region Teleport
    void OnTeleport() {
        if (TeleportCooldown > 0)
        {
            MiniBarPlace.SetActive(true);
            MiniBar.SetActive(true);
            MiniBar.transform.localScale = new Vector3(TeleportCooldown/(Cooldown - TeleportCooldownBonus),1f,1f);
            TeleportCooldown -= Time.deltaTime;
        }
        else {
            MiniBarPlace.SetActive(false);
            MiniBar.SetActive(false);
        }

        bool isCollidingWithWall = Physics2D.OverlapCircle(TeleportAim.transform.position, checkRadius, whatIsWall);

        if (Input.GetMouseButtonDown(0) && !isCollidingWithWall && TeleportCooldown <= 0 && MP >= 15)
        {
            MP -= 15;
            transform.position = TeleportAim.transform.position;
            TeleportCooldown = (Cooldown - TeleportCooldownBonus);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(TeleportAim.transform.position, checkRadius);
    }
    #endregion

    #region Summon

    void OnSummon()
    {

        if (SpawnCooldown > 0)
        {
            MiniBarPlace.SetActive(true);
            MiniBar.SetActive(true);
            MiniBar.transform.localScale = new Vector3(SpawnCooldown / SpawnMaxCooldown, 1f, 1f);
            SpawnCooldown -= Time.deltaTime;
        }
        else
        {
            MiniBarPlace.SetActive(false);
            MiniBar.SetActive(false);
        }

        bool isCollidingWithWall = Physics2D.OverlapCircle(PlayerAim.transform.position, checkRadius, whatIsWall);

        if (Input.GetMouseButton(0) && !isCollidingWithWall && SpawnCooldown <= 0 && MP >= 15)
        {
            Instantiate(SkeletonPrefab, PlayerAim.transform);
            MP -= 15;
            SpawnCooldown = SpawnMaxCooldown;
        }
    }

    #endregion

    #region Absorb
    void OnAbsorbBlood() {
        if (Input.GetMouseButtonDown(0)) {
            AudioController.current.PlayMusic(AudioController.current.spellSFX);
        }

        if (Input.GetMouseButton(0))
        {
            MP -= Time.deltaTime * 8;
            BloodDrain.SetActive(true);
        }
        else {
            BloodDrain.SetActive(false);
        }
    }

    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ManaSphere") {
            MP += 10;
            Destroy(collision.gameObject);
        }
    }
}
