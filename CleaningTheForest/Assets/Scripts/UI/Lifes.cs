using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lifes : MonoBehaviour
{
    public Image[] hearts;
    private Player player;

    public GameObject GameOverPanel;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < player.Lifes)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

        if (player.Lifes <= 0)
        {
            GameOverPanel.SetActive(true);
            Time.timeScale = 0;
        }
        else {
            GameOverPanel.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
