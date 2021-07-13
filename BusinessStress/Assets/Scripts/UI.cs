using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{

    public Player player;
    public Image HPBar;

    public Image Heart1;
    public Image Heart2;
    public Image Heart3;

    public Sprite HeartHappy;
    public Sprite HeartAngry;

    public Text Lifes;
    public Text LifesShadow;

    public Text Points;
    public Text PointsShadow;


    void Update()
    {
        HPBar.fillAmount = ((player.life) / (player.maxLife));

        if (HPBar.fillAmount == 1)
        {
            Heart1.sprite = HeartHappy;
            Heart2.sprite = HeartHappy;
            Heart3.sprite = HeartHappy;
        }
        else if (HPBar.fillAmount < 1 && HPBar.fillAmount >= 0.7)
        {
            Heart3.sprite = HeartAngry;
        }
        else if (HPBar.fillAmount < 0.7 && HPBar.fillAmount >= 0.4)
        {
            Heart2.sprite = HeartAngry;
        }
        else if (HPBar.fillAmount < 0.4) {
            Heart1.sprite = HeartAngry;
        }

        Lifes.text = "" + player.numLifes;
        LifesShadow.text = Lifes.text;

        if (player.numLifes < 0) {
            Lifes.text = "0";
            LifesShadow.text = Lifes.text;
        }


        Points.text = "" + Player.Points;
        PointsShadow.text = Points.text;
        
    }
}
