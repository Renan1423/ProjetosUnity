using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST}

public class BattleSystem : MonoBehaviour
{

    public GameObject playerPrefab;
    public Transform playerPrefabTrans;
    public GameObject enemyPrefab;
    public Transform enemyPrefabTrans;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    public GameObject PlayerCommandsGO;
    public Animator PlayerCommands;

    Unit playerUnit;
    Unit enemyUnit;

    public Text dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    private Animator anim;

    private int actionSelection;

    public BattleState state;

    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Commands();
        CameraAnimations();
        OnButton();
    }

    void CameraAnimations() {
        if (state == BattleState.PLAYERTURN)
        {
            anim.SetInteger("Turn", 1);
        }
        else {
            anim.SetInteger("Turn", 0);
        }
    }

    void Commands() {
        if (state == BattleState.PLAYERTURN)
        {
            PlayerCommandsGO.SetActive(true);
            if (Input.GetKey(KeyCode.UpArrow))
            {
                PlayerCommands.SetInteger("Selection", 1);
                actionSelection = 1;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                PlayerCommands.SetInteger("Selection", 2);
                actionSelection = 2;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                PlayerCommands.SetInteger("Selection", 3);
                actionSelection = 3;
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                PlayerCommands.SetInteger("Selection", 4);
                actionSelection = 4;
            }
        }
        else {
            PlayerCommandsGO.SetActive(false);
            PlayerCommands.SetInteger("Selection", 0);
            actionSelection = 0;
        }
    }

    IEnumerator SetupBattle() {

        GameObject playerGO = Instantiate(playerPrefab, playerPrefabTrans);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyPrefabTrans);
        enemyUnit = enemyGO.GetComponent<Unit>();
        dialogueText.text = "A wild " + enemyUnit.unitName + " approaches...";

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack(){
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = enemyUnit.unitName + " took damage!";

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn() {
        dialogueText.text = enemyUnit.unitName + " attacks!";

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        playerHUD.SetHP(playerUnit.currentHP);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            dialogueText.text = "You won the battle!";
        }
        else if (state == BattleState.LOST) {
            dialogueText.text = "Oh no! You were defeated!";
        }

    }

    void PlayerTurn() {
        dialogueText.text = "What should " + playerUnit.unitName + " do?";
    }

    IEnumerator PlayerHeal (){
        playerUnit.Heal(5);
        playerHUD.SetHP(playerUnit.currentHP);
        dialogueText.text = playerUnit.unitName + " feel renewed strength";

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());

    }

    void OnButton() {
        if (state != BattleState.PLAYERTURN)
        {
            return;
        }
        else {
            if (actionSelection == 0 && Input.GetKeyDown(KeyCode.Return))
            {
                return;
            }
            else if (actionSelection == 1 && Input.GetKeyDown(KeyCode.Return))
            {
                StartCoroutine(PlayerAttack());
                PlayerCommands.SetInteger("Selection", 0);
            }
            else if (actionSelection == 2 && Input.GetKeyDown(KeyCode.Return))
            {
                PlayerCommands.SetInteger("Selection", 0);
            }
            else if (actionSelection == 3 && Input.GetKeyDown(KeyCode.Return))
            {
                PlayerCommands.SetInteger("Selection", 0);
            }
            else if (actionSelection == 4 && Input.GetKey(KeyCode.Return)) {
                StartCoroutine(PlayerHeal());
                PlayerCommands.SetInteger("Selection", 0);
            }
        }
    }
}
