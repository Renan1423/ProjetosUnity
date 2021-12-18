using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodDrain : MonoBehaviour
{

    private bool isColliding;

    void Update()
    {
        if (isColliding)
        {
            Player.HP += (Time.deltaTime * (8 + Player.AbsorbHPBonus));
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            isColliding = true;
            collision.GetComponent<Enemy>().HP -= 1;
        }
        else {
            isColliding = false;
        }
    }
}
