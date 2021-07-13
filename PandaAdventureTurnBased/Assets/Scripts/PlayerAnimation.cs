using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    BattleSystem bs;

    private Animator anim;

    void Start()
    {
        bs = GetComponent<BattleSystem>();
        anim = GetComponent<Animator>();
        if (bs.state == BattleState.PLAYERTURN) { 
            anim.SetBool("PlayerTurn", true);
        }
    }

    void Update()
    {
        
    }
}
