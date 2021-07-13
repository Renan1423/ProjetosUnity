using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skills : MonoBehaviour
{
    public static float skill1Life;
    public static float skill2Life;
    public static float skill3Life;
    public static float skill4Life;
    public static float skill5Life;
    public static float maxSkillLife = 100;

    public float skill1Cooldown;
    public float skill2Cooldown;
    public float skill3Cooldown;
    public float skill4Cooldown;
    public float skill5Cooldown;

    public Image skill1;
    public Image skill2;
    public Image skill3;
    public Image skill4;
    public Image skill5;

    void Start()
    {
        skill1.fillAmount = 0;
        skill2.fillAmount = 0;
        skill3.fillAmount = 0;
        skill4.fillAmount = 0;
        skill5.fillAmount = 0;
    }

    void Update()
    {
        skill1.fillAmount = skill1Life / maxSkillLife;
        skill2.fillAmount = skill2Life / maxSkillLife;
        skill3.fillAmount = skill3Life / maxSkillLife;
        skill4.fillAmount = skill4Life / maxSkillLife;
        skill5.fillAmount = skill5Life / maxSkillLife;

        if (skill1Life < 0) 
        {
            skill1Life = 0;
        }
        if (skill2Life < 0)
        {
            skill2Life = 0;
        }
        if (skill3Life < 0)
        {
            skill3Life = 0;
        }
        if (skill4Life < 0)
        {
            skill4Life = 0;
        }
        if (skill5Life < 0)
        {
            skill5Life = 0;
        }
    }

    void FixedUpdate()
    {
        if (skill1Life <= maxSkillLife)
        {
            skill1Life += skill1Cooldown;
        }
        if (skill2Life <= maxSkillLife)
        {
            skill2Life += skill2Cooldown;
        }
        if (skill3Life <= maxSkillLife)
        {
            skill3Life += skill3Cooldown;
        }
        if (skill4Life <= maxSkillLife)
        {
            skill4Life += skill4Cooldown;
        }
        if (skill5Life <= maxSkillLife)
        {
            skill5Life += skill5Cooldown;
        }
    }

    public void FirstSkill() {
        skill1Life = 0;
        skill2Life -= 15;
        skill3Life -= 15;
        skill4Life -= 15;
        skill5Life -= 15;
    }

    public void SecondSkill()
    {
        skill2Life = 0;
        skill1Life -= 30;
        skill3Life -= 30;
        skill4Life -= 30;
        skill5Life -= 30;
    }

    public void ThirdSkill()
    {
        skill3Life = 0;
        skill2Life -= 10;
        skill1Life -= 10;
        skill4Life -= 10;
        skill5Life -= 10;
    }

    public void FourthSkill()
    {
        skill4Life = 0;
        skill2Life -= 15;
        skill3Life -= 15;
        skill1Life -= 15;
        skill5Life -= 15;
    }

    public void FifthSkill()
    {
        skill5Life = 0;
        skill2Life -= 10;
        skill3Life -= 10;
        skill4Life -= 10;
        skill1Life -= 10;
    }
}
