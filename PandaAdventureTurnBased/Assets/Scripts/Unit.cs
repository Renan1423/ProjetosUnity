using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;

    public int damage;
    private int dmgCount;

    public int maxHP;
    public int currentHP;

    public bool TakeDamage(int dmg) {
        
        while (dmgCount < dmg)
        {
            currentHP--;
            dmgCount++;
            if (dmgCount >= dmg) {
                dmgCount = 0;
                break;
            }
        }

        //currentHP -= dmg;

        if (currentHP <= 0)
        {
            return true;
        }
        else {
            return false;
        }
    }

    public void Heal(int amount) {
        currentHP += amount;
        if (currentHP > maxHP) {
            currentHP = maxHP;
        }
    }

}
