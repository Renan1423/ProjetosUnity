using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    
    private Animator anim;
    public GameObject player;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<Player>().life == 1)
        {
            anim.SetBool("isCritical", true);
        }
        else {
            anim.SetBool("isCritical", false);
        };
    }
}
