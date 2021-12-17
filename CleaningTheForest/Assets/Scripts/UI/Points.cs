using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Points : MonoBehaviour
{
    private Player player;

    public Text pointText;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        pointText.text = "         " + player.Points;

        if (player.Points < 10) {
            pointText.text = "          00000" + player.Points;
        }

        if (player.Points >= 10 && player.Points < 100)
        {
            pointText.text = "          0000" + player.Points;
        }

        if (player.Points >= 100 && player.Points < 1000)
        {
            pointText.text = "          000" + player.Points;
        }

        if (player.Points >= 1000 && player.Points < 10000)
        {
            pointText.text = "          00" + player.Points;
        }

        if (player.Points >= 10000 && player.Points < 100000)
        {
            pointText.text = "          0" + player.Points;
        }

        if (player.Points >= 100000 && player.Points < 1000000)
        {
            pointText.text = "          " + player.Points;
        }

        if (player.Points >= 999999) {
            pointText.text = "           999999";
        }

    }
}
