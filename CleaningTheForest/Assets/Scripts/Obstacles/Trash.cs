using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    private Rigidbody2D rig;
    private Transform trans;
    private Animator anim;
    private SpriteRenderer spr;
    private BoxCollider2D col;

    private GameObject MovePointPosition;
    private Player player;
    private CameraController vCam;

    [Header ("Status")]

    public float Speed;
    public int TrashPosition = 3;

    public int life = 1;
    public int maxLife = 1;

    [Header("Heavy Object")]

    public bool isHeavy;

    [Header ("Slight Object")]

    public bool isSlight = false;
    private float HorizontalSpeed = 0;

    private float rotationZ = 0;
    public float rotationSpeed = 0;

    [Header("Unbreakable Object")]

    public bool isUnbreakable;


    //public Sprite damageSprite;

    private SpeedController spdController;

    void Start()
    {
        trans = GetComponent<Transform>();
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        col = GetComponent<BoxCollider2D>();

        vCam = FindObjectOfType<CameraController>();

        player = FindObjectOfType<Player>();

        TrashPosition = Random.Range(1, 6);

        maxLife = life;

        MovePointPosition = GameObject.Find("MovePoint" + TrashPosition);

        trans.position = new Vector2 (MovePointPosition.transform.position.x,trans.position.y);

        if (isHeavy) {
            if (Speed > 2)
            {
                Speed -= 1;
            }
            else {
                Speed = 0.75f;
            }
        }

        if (isSlight)
        {
            if (TrashPosition == 1 || TrashPosition == 2) {
                rotationSpeed = rotationSpeed * -1;
            }
            
        }

        spdController = FindObjectOfType<SpeedController>();
    }

    void Update()
    {

        OnSlowTime();

        if (isSlight) {
            if (TrashPosition == 5)
            {
                HorizontalSpeed = -1;
            }
            else if (TrashPosition == 1)
            {
                HorizontalSpeed = 1;
            }
            else if (TrashPosition == 2)
            {
                HorizontalSpeed = 0.5f;
            }
            else if (TrashPosition == 4)
            {
                HorizontalSpeed = -0.5f;
            }
            else {
                HorizontalSpeed = 0f;
            }
        }

        if (isSlight)
        {
            rig.velocity = new Vector2(HorizontalSpeed, Speed);
            rotationZ += Time.deltaTime * rotationSpeed;

            transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, rotationZ);
        }
        else {
            rig.velocity = new Vector2(rig.velocity.x, Speed);
        }

        if (life < maxLife) {
            //spr.sprite = damageSprite;
            anim.SetBool("isDamaged", true);
        }
        
    }

    void OnSlowTime() {
        if (player.playerPowerUp == PowerUps.TIME)
        {
            Speed = spdController.objSpeed / 2;
            anim.speed = 0.5f;
        }
        else {
            Speed = spdController.objSpeed;
            anim.speed = 1f;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "AtkPoint") {

            if (!isUnbreakable)
            {
                life--;
                player.comboCount++;
                player.comboPanel.GetComponent<Animator>().SetTrigger("Increase");
            }
            else if (isUnbreakable && player.playerPowerUp == PowerUps.GIANT) {
                life = 0;
            }
            
            if (life <= 0)
            {
                Speed = 0;
                player.Points += 5;
                AudioController.current.PlayMusic(AudioController.current.Click);
                AudioController.current.PlayMusic(AudioController.current.DestroyObstacle);
                col.enabled = false;
                anim.SetBool("isDead", true);
                Destroy(gameObject, 0.7f);
                
            }
            
            else {
                AudioController.current.PlayMusic(AudioController.current.DestroyObstacle);
                vCam.CameraSmallShake();
                if (!isUnbreakable)
                {
                    player.Points += 5;
                    player.isAttacking = false;
                    if (player.playerPowerUp != PowerUps.TRUCK)
                    {
                        collision.gameObject.SetActive(false);
                    }
                }
            }
        }

        if (collision.tag == "Wave") {

            if (!isUnbreakable)
            {
                life -= 2;
                
            }
            else {
                // Destroy Unbreakable object
                life = 0;
            }

            if (life <= 0)
            {
                Speed = 0;
                player.Points += 5;
                AudioController.current.PlayMusic(AudioController.current.Click);
                AudioController.current.PlayMusic(AudioController.current.DestroyObstacle);
                col.enabled = false;
                anim.SetBool("isDead", true);
                Destroy(gameObject, 0.7f);

            }


            collision.GetComponent<Wave>().life--;

            if (collision.GetComponent<Wave>().life <= 0)
            {
                collision.GetComponent<Animator>().SetBool("isDead", true);
                collision.GetComponent<Collider2D>().enabled = false;
                collision.GetComponent<Wave>().speed = 0;
                Destroy(collision.gameObject, 0.8f);
            }

        }

        if (collision.tag == "Tsunami")
        {

            if (!isUnbreakable)
            {
                life -= 2;

            }
            else
            {
                // Destroy Unbreakable object
                life = 0;
            }

            if (life <= 0)
            {
                Speed = 0;
                player.Points += 5;
                AudioController.current.PlayMusic(AudioController.current.Click);
                AudioController.current.PlayMusic(AudioController.current.DestroyObstacle);
                col.enabled = false;
                anim.SetBool("isDead", true);
                Destroy(gameObject, 0.7f);

            }

        }

    }
}
