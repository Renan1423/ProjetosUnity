using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    private BattleSystem bs;

    private Animator anim;

    void Start()
    {
        bs = FindObjectOfType<BattleSystem>().GetComponent<BattleSystem>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        OnPlayerTurn();
        OnEnemyTurn();
    }

    void OnPlayerTurn() {
        if (bs.state == BattleState.PLAYERTURN)
        {
            anim.SetBool("PlayerTurn", true);
            anim.SetInteger("CommandSelect", 0);

            switch (bs.actionSelection)
            {
                case 0:
                    anim.SetInteger("CommandSelect", 0);
                    break;
                case 1:
                    anim.SetInteger("CommandSelect", 1);
                    break;
                case 2:
                    anim.SetInteger("CommandSelect", 2);
                    break;
                case 3:
                    anim.SetInteger("CommandSelect", 3);
                    break;
                case 4:
                    anim.SetInteger("CommandSelect", 4);
                    break;
                default:
                    anim.SetInteger("CommandSelect", 0);
                    break;
            }
        }
        else
        {
            anim.SetInteger("CommandSelect", 0);
            anim.SetBool("PlayerTurn", false);
            
        }
    }

    void OnEnemyTurn() { 
        
    }
}
