using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUps {NORMAL, GIANT, TRUCK, TIME}

public class Player : MonoBehaviour
{
    private Transform trans;
    private BoxCollider2D col;

    public Animator PlayerGFX;

    public bool Started;

    [Header("Attack")]

    public GameObject AtkPoint;
    public bool isAttacking;

    private bool isStumbling;

    [Header("Movement")]

    public int PlayerPositionX = 2;

    public Transform[] MovePointsX;

    public int PlayerPositionY = 0;

    public Transform[] MovePointsY;

    [Header("UI")]

    public int Points;
    public int Lifes = 3;
    public int comboCount = 0;
    public int comboLimit = 100;

    public GameObject comboPanel;

    [Header("Powers")]

    public bool isShielded;

    [Header("Power-Up")]

    private SpeedController spdController;
    private float normalSpeed; //Speed Controller's speed before using the truck power up

    public PowerUps playerPowerUp;
    public float powerUpDuration;
    public float powerUpDurationCount;

    public GameObject GarbageTruck;

    public GameObject SlowTimeScreen;



    void Start()
    {
        trans = GetComponent<Transform>();
        col = GetComponent<BoxCollider2D>();

        spdController = FindObjectOfType<SpeedController>();

        playerPowerUp = PowerUps.NORMAL;
    }

    void Update()
    {
        if (Started)
        {

            //Attack

            if (playerPowerUp != PowerUps.TRUCK)
            {
                OnAttack();
            }

            //Movement

            OnMoveX();

            OnMoveY();

            //Changing Speed

            ChangeSpeed();

        }

        //Combo

        Combo();

        //Power Ups

        OnPowerUp();
    }

    #region Move
    void OnMoveX()
    {
        if (!isAttacking && !isStumbling)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (PlayerPositionX < MovePointsX.Length - 1)
                {
                    PlayerPositionX += 1;
                }
                else
                {
                    PlayerPositionX = 5;
                }
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (PlayerPositionX > 0)
                {
                    PlayerPositionX -= 1;
                }
                else
                {
                    PlayerPositionX = 0;
                }
            }
        }

        //Changing the position

        trans.position = new Vector2(MovePointsX[PlayerPositionX].position.x, trans.position.y);
    }


    void OnMoveY()
    {
        if (!isAttacking && !isStumbling)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (PlayerPositionY < MovePointsY.Length - 1)
                {
                    PlayerPositionY += 1;
                }
                else
                {
                    PlayerPositionY = 3;
                }
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (PlayerPositionY > 0)
                {
                    PlayerPositionY -= 1;
                }
                else
                {
                    PlayerPositionY = 0;
                }
            }
        }

        //Changing the position

        trans.position = new Vector2(trans.position.x, MovePointsY[PlayerPositionY].position.y);
    }
    #endregion

    #region Attack

    void OnAttack() {
        if (Input.GetKeyDown(KeyCode.Space) && !isStumbling) {
            PlayerGFX.SetTrigger("Attack");
            isAttacking = true;
            AtkPoint.SetActive(true);
            PlayerGFX.speed = 1;
        }

        if (isAttacking && !isStumbling)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !isStumbling)
            {
                PlayerGFX.SetTrigger("Attack");
                AtkPoint.SetActive(true);
                StartCoroutine(FinishAttack());
                PlayerGFX.speed = 1;
            }
            else
            {
                StartCoroutine(FinishAttack());
            }
        }

        if (isAttacking)
        {
            PlayerGFX.speed = 1;
            AtkPoint.SetActive(true);
        }
        else {
            AtkPoint.SetActive(false);
        }


        //Stop attacking if the player is stumbling

        if (isStumbling) {
            AtkPoint.SetActive(false);
            StopCoroutine(FinishAttack());
        }
    }

    IEnumerator FinishAttack() {

        if (Input.GetKeyDown(KeyCode.Space) && !isStumbling)
        {
            PlayerGFX.SetTrigger("Attack");
            isAttacking = true;
            AtkPoint.SetActive(true);
            //StopCoroutine(FinishAttack());
        }

        PlayerGFX.speed = 1;

        yield return new WaitForSeconds(0.5f);

        isAttacking = false;

        PlayerGFX.speed = 1;

        if (Input.GetKeyDown(KeyCode.Space) && !isStumbling)
        {
            PlayerGFX.SetTrigger("Attack");
            isAttacking = true;
            AtkPoint.SetActive(true);
            PlayerGFX.speed = 1;
            //StopCoroutine(FinishAttack());
        }

        yield return new WaitForSeconds(0.1f);

        PlayerGFX.speed = 1;

        //AtkPoint.SetActive(false);

        if (Input.GetKeyDown(KeyCode.Space) && !isStumbling)
        {
            PlayerGFX.SetTrigger("Attack");
            isAttacking = true;
            AtkPoint.SetActive(true);
            PlayerGFX.speed = 1;
            //StopCoroutine(FinishAttack());
        }

        //StopAllCoroutines();
        StopCoroutine(FinishAttack());

    }

    void Combo()
    {
        if (comboCount >= 2)
        {
            comboPanel.SetActive(true);
        }
        else
        {
            comboPanel.SetActive(false);
        }
    }

    #endregion

    #region Speed
    void ChangeSpeed() {
        if (!isAttacking)
        {
            if (playerPowerUp != PowerUps.TIME)
            {
                PlayerGFX.speed = 1f + (0.05f * (Points / 50));
                if (PlayerGFX.speed >= 2.5f)
                {
                    PlayerGFX.speed = 2.5f;
                }
            }
            else {
                PlayerGFX.speed = 0.75f + (0.05f * (Points / 50));
                if (PlayerGFX.speed >= 2f)
                {
                    PlayerGFX.speed = 2f;
                }
            }
        }
        else {
            PlayerGFX.speed = 1;
        }
    }
    #endregion

    #region Stumble
    IEnumerator OnStumble() {

        isStumbling = true;
        PlayerGFX.SetBool("Stumble", true);
        col.enabled = false;

        yield return new WaitForSeconds(0.001f);

        PlayerGFX.SetBool("Stumble", false);
        //PlayerGFX.Play("PampamWalk");

        yield return new WaitForSeconds(1f);

        isStumbling = false;
        col.enabled = true;

        StopCoroutine(OnStumble());

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Stone" && !isStumbling && playerPowerUp != PowerUps.TRUCK) {

            isAttacking = false;
            AtkPoint.SetActive(false);
            //StopCoroutine(FinishAttack());
            collision.GetComponent<Collider2D>().enabled = false;
            StartCoroutine(OnStumble());
            
        }
    }
    #endregion

    #region Power-Ups

    void OnPowerUp() {

        if (playerPowerUp != PowerUps.NORMAL)
        {

            switch (playerPowerUp) {

                case PowerUps.GIANT:

                    trans.localScale = new Vector3(2f, 2f, 2f);
                    GarbageTruck.SetActive(false);
                    SlowTimeScreen.SetActive(false);

                    break;

                case PowerUps.TRUCK:

                    trans.localScale = new Vector3(1f, 1f, 1f);
                    SlowTimeScreen.SetActive(false);
                    normalSpeed = spdController.GetComponent<SpeedController>().objSpeed;
                    spdController.GetComponent<SpeedController>().objSpeed = normalSpeed + 1f;
                    GarbageTruck.SetActive(true);

                    break;

                case PowerUps.TIME:

                    trans.localScale = new Vector3(1f, 1f, 1f);
                    GarbageTruck.SetActive(false);
                    SlowTimeScreen.SetActive(true);

                    break;

                default:

                    break;
            }

            powerUpDurationCount += Time.deltaTime;

            if (powerUpDurationCount >= powerUpDuration) {
                powerUpDurationCount = 0;
                spdController.GetComponent<SpeedController>().objSpeed = normalSpeed;
                playerPowerUp = PowerUps.NORMAL;
            }


        }
        else {
            powerUpDurationCount = 0;
            trans.localScale = new Vector3(1f, 1f, 1f);
            GarbageTruck.SetActive(false);
            SlowTimeScreen.SetActive(false);
        }

    }

    #endregion
}
